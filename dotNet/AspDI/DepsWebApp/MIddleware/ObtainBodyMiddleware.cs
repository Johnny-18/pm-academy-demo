using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DepsWebApp.MIddleware
{
    /// <summary>
    /// Middleware for obtain body from request and response
    /// </summary>
    public static class ObtainBodyMiddleware
    {
        /// <summary>
        /// Method for obtain request body
        /// </summary>
        /// <param name="request">Http context</param>
        /// <returns>Request body</returns>
        public static async Task<string> ObtainRequestBody(HttpRequest request)
        {
            if (request.Body == null) 
                return string.Empty;
            
            request.EnableBuffering();
            
            var encoding = GetEncodingFromContentType(request.ContentType);
            
            string bodyStr;
            using (var reader = new StreamReader(request.Body, encoding, true, 1024, true))
            {
                bodyStr = await reader.ReadToEndAsync().ConfigureAwait(false);
            }
            
            request.Body.Seek(0, SeekOrigin.Begin);return bodyStr;
        }

        /// <summary>
        /// Method for obtain request body
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns>Response body</returns>
        public static async Task<string> ObtainResponseBody(HttpContext context)
        {
            var response = context.Response;
            
            response.Body.Seek(0, SeekOrigin.Begin);
            
            var encoding = GetEncodingFromContentType(response.ContentType);
            using (var reader = new StreamReader(response.Body, encoding, detectEncodingFromByteOrderMarks: false,
                bufferSize: 4096, leaveOpen: true))
            {
                var text = await reader.ReadToEndAsync().ConfigureAwait(false);
                response.Body.Seek(0, SeekOrigin.Begin);
                return text;
            }
        }

        private static Encoding GetEncodingFromContentType(string contentTypeStr)
        {
            if (string.IsNullOrEmpty(contentTypeStr))
            {
                return Encoding.UTF8;
            }
            
            ContentType contentType;
            try
            {
                contentType = new ContentType(contentTypeStr);
            }
            catch (FormatException)
            {
                return Encoding.UTF8;
            }
            
            if (string.IsNullOrEmpty(contentType.CharSet))
            {
                return Encoding.UTF8;
            }
            
            return Encoding.GetEncoding(contentType.CharSet, EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback);
        }
    }
}