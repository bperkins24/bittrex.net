using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Api;
using Microsoft.AspNetCore.Mvc;

namespace ApocoCrypto.Web.Api.Controllers.Bittrex
{
    [Route("api/[controller]")]
    public class BittrexMarketSummaryController : Controller
    {
        private readonly IBittrexClient _bittrexClient;

        public BittrexMarketSummaryController(IBittrexClient bittrexClient)
        {
            _bittrexClient = bittrexClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string marketName)
        {
            return Ok(await _bittrexClient.GetMarketSummary(marketName));
        }
    }
}