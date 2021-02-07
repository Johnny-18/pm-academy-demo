using System;
using System.Net;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;

namespace RequestProcessor.App.Services
{
    /// <summary>
    /// Request performer.
    /// </summary>
    internal class RequestPerformer : IRequestPerformer
    {
        private readonly IRequestHandler _requestHandler;
        private readonly IResponseHandler _responseHandler;
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="requestHandler">Request handler implementation.</param>
        /// <param name="responseHandler">Response handler implementation.</param>
        /// <param name="logger">Logger implementation.</param>
        public RequestPerformer(
            IRequestHandler requestHandler, 
            IResponseHandler responseHandler,
            ILogger logger)
        {
            _requestHandler = requestHandler;
            _responseHandler = responseHandler;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<bool> PerformRequestAsync(IRequestOptions requestOptions, IResponseOptions responseOptions)
        {
            if (requestOptions == null)
                throw new ArgumentNullException(nameof(requestOptions));
            if (!requestOptions.IsValid)
                throw new ArgumentOutOfRangeException(nameof(requestOptions));

            try
            {
                IResponse response;
                try
                {
                    response = await _requestHandler.HandleRequestAsync(requestOptions);

                    _logger.Log($"Sending request to {requestOptions.Address}" +
                                $"\nGet response: status code {response.Code}, handled: {response.Handled} content: {response.Content}");
                }
                catch (TimeoutException e)
                {
                    _logger.Log(e, e.Message);
                    response = new Response(false, (int) HttpStatusCode.RequestTimeout, "");
                }


                await _responseHandler.HandleResponseAsync(response, requestOptions, responseOptions);
                _logger.Log($"Save request to {responseOptions.Path}");
                
                return true;
            }
            catch (Exception e)
            {
                _logger.Log(e, e.Message);
                throw new PerformException("Something going wrong!", e);
            }
        }
    }
}
