using System;
using System.Linq;
using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Api;
using Microsoft.AspNetCore.Mvc;

namespace ApocoCrypto.Web.Api.Controllers.Bittrex
{
    [Route("api/[controller]")]
    public class BittrexMarketsController : Controller
    {
        private readonly IBittrexClient _bittrexClient;

        public BittrexMarketsController(IBittrexClient bittrexClient)
        {
            _bittrexClient = bittrexClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string baseCurrency = null)
        {
            var items = await _bittrexClient.GetMarkets();
            if (baseCurrency == null)
                return Ok(items);

            return Ok(items.Where(i =>
                string.Equals(i.BaseCurrency, baseCurrency, StringComparison.OrdinalIgnoreCase)));
        }
    }
}