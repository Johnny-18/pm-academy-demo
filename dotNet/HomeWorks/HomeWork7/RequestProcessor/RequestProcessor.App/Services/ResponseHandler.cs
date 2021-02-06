using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class ResponseHandler : IResponseHandler
    {
        Task IResponseHandler.HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            //todo save result in file 
            throw new System.NotImplementedException();
        }
    }
}