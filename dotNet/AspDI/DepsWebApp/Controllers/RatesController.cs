using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DepsWebApp.Services;

namespace DepsWebApp.Controllers
{
    /// <summary>
    /// Controller for rates calculation
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly ILogger<RatesController> _logger;
        private readonly IRatesService _rates;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public RatesController(
            IRatesService rates,
            ILogger<RatesController> logger)
        {
            _rates = rates;
            _logger = logger;
        }

        /// <summary>
        /// Exchanges given amount from source currency to destination currency
        /// </summary>
        /// <param name="srcCurrency">Source currency</param>
        /// <param name="dstCurrency">Destination currency</param>
        /// <param name="amount">Amount of funds</param>
        /// <returns>Returns exchange result or <c>null</c> if source or destination currency wasn't found</returns>
        [HttpGet("{srcCurrency}/{dstCurrency}")]
        public async Task<ActionResult<decimal>> Get(string srcCurrency, string dstCurrency, decimal? amount)
        {
            var exchange =  await _rates.ExchangeAsync(srcCurrency, dstCurrency, amount ?? decimal.One);
            if (!exchange.HasValue)
            {
                _logger.LogDebug($"Can't exchange from '{srcCurrency}' to '{dstCurrency}'");
                return BadRequest("Invalid currency code");
            }
            return exchange.Value.DestinationAmount;
        }
    }
}
