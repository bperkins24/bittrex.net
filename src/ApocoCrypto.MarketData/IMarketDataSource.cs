using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApocoCrypto.MarketData
{
    public interface IMarketDataSource
    {
        Task<IEnumerable<IMarket>> GetMarketsAsync();

        Task<IMarket> GetMarketAsync(string name);

        Task<IEnumerable<IMarketSummary>> GetMarketSummariesAsync(string baseCurrency);

        Task<IEnumerable<ITick>> GetTicksAsync(string marketName, string tickInterval);
    }
}