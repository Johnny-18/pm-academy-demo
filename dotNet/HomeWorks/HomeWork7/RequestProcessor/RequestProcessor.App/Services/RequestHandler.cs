using System.Net.Http;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class RequestHandler : IRequestHandler
    {
        private HttpClient _client;
        
        public RequestHandler(HttpClient client)
        {
            _client = client;
        }

        async Task<IResponse> IRequestHandler.HandleRequestAsync(IRequestOptions requestOptions)
        {
            var res = await _client.GetAsync("");
            //todo
            throw new System.NotImplementedException();
        }
    }
}