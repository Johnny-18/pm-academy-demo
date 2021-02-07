using System;
using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class RequestHandler : IRequestHandler
    {
        private readonly HttpClient _client;
        
        public RequestHandler(HttpClient client)
        {
            _client = client;
        }

        async Task<IResponse> IRequestHandler.HandleRequestAsync(IRequestOptions requestOptions)
        {
            if (requestOptions == null)
                throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid)
                throw new ArgumentOutOfRangeException(nameof(requestOptions));
            
            using var requestMessage = new HttpRequestMessage(MapMethods(requestOptions.Method),
                new Uri(requestOptions.Address));
            
            var responseMessage = await _client.SendAsync(requestMessage);

            return new Response(true, (int) responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
        }

        private HttpMethod MapMethods(RequestMethod method)
        {
            switch (method)
            {
                case RequestMethod.Get:
                    return HttpMethod.Get;
                case RequestMethod.Delete:
                    return HttpMethod.Delete;
                case RequestMethod.Post:
                    return HttpMethod.Post;
                case RequestMethod.Put:
                    return HttpMethod.Put;
                case RequestMethod.Patch:
                    return HttpMethod.Patch;
                case RequestMethod.Undefined:
                    throw new ArgumentOutOfRangeException(nameof(method));
                default:
                    return null;
            }
        }
    }
}