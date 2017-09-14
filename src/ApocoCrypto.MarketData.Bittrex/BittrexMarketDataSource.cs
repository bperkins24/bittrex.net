using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Api;

namespace ApocoCrypto.MarketData.Bittrex
{
    public class BittrexMarketDataSource : IMarketDataSource
    {
        private readonly IBittrexClient _client;

        public BittrexMarketDataSource(IBittrexClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<IMarket>> GetMarketsAsync()
        {
            return await _client.GetMarkets();
        }

        public async Task<IMarket> GetMarketAsync(string name)
        {
            var markets = await _client.GetMarkets();

            return markets.FirstOrDefault(m => string.Equals(m.MarketName, name, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<IMarketSummary>> GetMarketSummariesAsync(string baseCurrency)
        {
            var markets = await _client.GetMarkets();
            var baseMarkets = markets.Where(m =>
                string.Equals(m.BaseCurrency, baseCurrency, StringComparison.OrdinalIgnoreCase));

            var summaries = await _client.GetMarketSummaries();

            return summaries.Where(m => baseMarkets.Any(bm => bm.MarketName == m.MarketName));
        }

        public async Task<IEnumerable<ITick>> GetTicksAsync(string marketName, string tickInterval)
        {
            return await _client.GetTicks(marketName, tickInterval);
        }
    }
}