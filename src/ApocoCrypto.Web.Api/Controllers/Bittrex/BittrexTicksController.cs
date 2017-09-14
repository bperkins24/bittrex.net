using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Api;
using Microsoft.AspNetCore.Mvc;

namespace ApocoCrypto.Web.Api.Controllers.Bittrex
{
    [Route("api/[controller]")]
    public class BittrexTicksController : Controller
    {
        private readonly IBittrexClient _bittrexClient;

        public BittrexTicksController(IBittrexClient bittrexClient)
        {
            _bittrexClient = bittrexClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string marketName, string tickInterval)
        {
            return Ok(await _bittrexClient.GetTicks(marketName, tickInterval));
        }
    }
}