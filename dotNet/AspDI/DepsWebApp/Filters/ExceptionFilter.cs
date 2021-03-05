using System;
using System.Text.Json;
using DepsWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DepsWebApp.Filters
{
    /// <summary>
    /// Custom exception filter for swagger
    /// </summary>
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        /// <summary>
        /// Method for handling error messages
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            // _logger.LogInformation(await ObtainRequestBody(context.Request));
            // var originalBodyStream = context.Response.Body;
            // await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            // context.Response.Body = responseBody;
            // await _next.Invoke(context);
            // _logger.LogInformation(await ObtainResponseBody(context));
            // await responseBody.CopyToAsync(originalBodyStream);
           
            string exceptionMessage = context.Exception.Message;
            var randomCode = new Random();

            var excResult = new ExceptionResult
            {
                Code = randomCode.Next(1, 501),
                Message = exceptionMessage
            };

            context.Result = new ContentResult
            {
                Content = JsonSerializer.Serialize(excResult)
            };
            context.ExceptionHandled = true;
        }
    }
}