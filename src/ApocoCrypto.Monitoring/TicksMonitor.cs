using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApocoCrypto.MarketData;

namespace ApocoCrypto.Monitoring
{
    public class TicksMonitor
    {
        private readonly IMarketDataSource _mds;

        public TicksMonitor(IMarketDataSource mds)
        {
            _mds = mds;
        }

        public async Task<IEnumerable<Volume>> Get(MonitorOptions options)
        {
            var marketSummaries = await _mds.GetMarketSummariesAsync("BTC");

            var tasks = new List<Task>();
            var ticktionary = new ConcurrentDictionary<IMarketSummary, IEnumerable<ITick>>();
            foreach (var m in marketSummaries)
            {
                var task = _mds.GetTicksAsync(m.MarketName, options.TickInterval)
                    .ContinueWith(r => ticktionary.TryAdd(m, r.Result));

                tasks.Add(task);
            }

            await Task.WhenAll(tasks);

            var items = new List<Volume>();

            foreach (var kvp in ticktionary)
            {
                //get avg base volume
                var intervalAvgVol = kvp.Value
                    .Where(t => t.IsInIntervalWindow(options.TickInterval, options.IntervalWindow))
                    .Average(r => r.BaseVolume);
                var curVol = kvp.Key.BaseVolume;
                var percentDiff = (curVol - intervalAvgVol) / intervalAvgVol;

                if (percentDiff > options.Threshold)
                    items.Add(new Volume(kvp.Key.MarketName, intervalAvgVol, curVol, percentDiff));
            }

            return items;
        }
    }

    public class MonitorOptions
    {
        public string TickInterval { get; set; }

        public double IntervalWindow { get; set; }

        public decimal Threshold { get; set; }

        public MonitorOptions(string tickInterval, double intervalWindow, decimal threshold)
        {
            TickInterval = tickInterval;
            IntervalWindow = intervalWindow;
            Threshold = threshold;
        }
    }

    public class Volume
    {
        public string MarketName { get; set; }

        public decimal IntervalVolume { get; set; }

        public decimal CurrentVolume { get; set; }

        public decimal PercentDiff { get; set; }

        public Volume(string marketName, decimal intervalVolume, decimal currentVolume, decimal percentDiff)
        {
            MarketName = marketName;
            IntervalVolume = intervalVolume;
            CurrentVolume = currentVolume;
            PercentDiff = percentDiff;
        }
    }
}