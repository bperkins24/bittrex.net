using System.Threading.Tasks;
using ApocoCrypto.Monitoring;
using Microsoft.AspNetCore.Mvc;

namespace ApocoCrypto.Web.Api.Controllers.Bittrex
{
    [Route("api/[controller]")]
    public class BittrexVolumeController : Controller
    {
        private readonly TicksMonitor _monitor;

        public BittrexVolumeController(TicksMonitor monitor)
        {
            _monitor = monitor;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string tickInterval, int intervalWindow, decimal threshold)
        {
            return Ok(_monitor.Get(new MonitorOptions(tickInterval, intervalWindow, threshold)));
        }
    }
}