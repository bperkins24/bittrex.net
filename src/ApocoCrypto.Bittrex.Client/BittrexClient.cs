using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApocoCrypto.Bittrex.Client.Response;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace ApocoCrypto.Bittrex.Client
{
    public class BittrexClient
    {
        private const string Apiv1 = "api/v1.1/";
        private const string Apiv2 = "api/v2.0/";

        private static HttpClient Client;

        private readonly BittrexClientConfig _config;

        public BittrexClient(BittrexClientConfig config)
        {
            _config = config;
            Client = new HttpClient(_config.Handler ?? new HttpClientHandler(), _config.Handler != null);
            Client.BaseAddress = config.Url;
        }

        public async Task<BittrexList<Market>> GetMarkets()
        {
            return await GetList<Market>("public/getmarkets");
        }

        public async Task<BittrexList<CurrencyResponse>> GetCurrencies()
        {
            return await GetList<CurrencyResponse>("public/getcurrencies");
        }

        public async Task<BittrexSingle<Quote>> GetTicker(string marketName)
        {
            return await GetSingle<Quote>("public/getticker", new Dictionary<string, string> {{"market", marketName } });
        }

        public async Task<BittrexList<MarketSummary>> GetMarketSummaries()
        {
            return await GetList<MarketSummary>("public/getmarketsummaries");
        }

        public async Task<BittrexList<MarketSummary>> GetMarketSummary(string marketName)
        {
            return await GetList<MarketSummary>("public/getmarketsummary", new Dictionary<string, string> { { "market", marketName } });
        }

        public async Task<BittrexSingle<FullOrderBook>> GetFullOrderBook(string marketName)
        {
            return await GetSingle<FullOrderBook>("public/getorderbook", new Dictionary<string, string> { { "market", marketName }, { "type", "both" } });
        }

        public async Task<BittrexList<OrderBookEntry>> GetOrderBook(string marketName, string type)
        {
            return await GetList<OrderBookEntry>("public/getorderbook", new Dictionary<string, string> { { "market", marketName }, { "type", type } });
        }

        public async Task<BittrexList<MarketHistory>> GetMarketHistory(string marketName)
        {
            return await GetList<MarketHistory>("public/getmarkethistory", new Dictionary<string, string> { { "market", marketName } });
        }

        public async Task<BittrexList<Tick>> GetTicks(string marketName, string tickInterval)
        {
            return await GetList<Tick>(Apiv2, "pub/market/GetTicks", new Dictionary<string, string> { { "marketName", marketName }, { "tickInterval", tickInterval } });
        }

        public async Task<BittrexList<T>> GetList<T>(string url, IDictionary<string, string> query = null)
        {
            return await Get<BittrexList<T>>(Apiv1, url, query);
        }

        public async Task<BittrexList<T>> GetList<T>(string baseUrl, string url, IDictionary<string, string> query = null)
        {
            return await Get<BittrexList<T>>(baseUrl, url, query);
        }

        public async Task<BittrexSingle<T>> GetSingle<T>(string url, IDictionary<string, string> query = null)
        {
            return await Get<BittrexSingle<T>>(Apiv1, url, query);
        }

        public async Task<T> Get<T>(string baseUrl, string url, IDictionary<string, string> query = null)
        {
            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            var response = await Client.GetAsync(baseUrl + url);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}