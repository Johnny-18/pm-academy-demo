using System;
using System.IO;
using System.Threading.Tasks;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    public class ResponseHandler : IResponseHandler
    {
        async Task IResponseHandler.HandleResponseAsync(IResponse response, IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (response == null || requestOptions == null)
                throw new ArgumentNullException();
            
            var content = $"Status code: {response.Code}, handled: {response.Handled}" +
                          $"\n{response.Content}";
            
            await File.WriteAllTextAsync(responseOptions.Path, content);
        }
    }
}