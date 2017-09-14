using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Api;
using Microsoft.AspNetCore.Mvc;

namespace ApocoCrypto.Web.Api.Controllers.Bittrex
{
    [Route("api/[controller]")]
    public class BittrexMarketSummariesController : Controller
    {
        private readonly IBittrexClient _bittrexClient;

        public BittrexMarketSummariesController(IBittrexClient bittrexClient)
        {
            _bittrexClient = bittrexClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bittrexClient.GetMarketSummaries());
        }
    }
}