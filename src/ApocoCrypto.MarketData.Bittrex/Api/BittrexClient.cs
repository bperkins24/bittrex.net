using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ApocoCrypto.MarketData.Bittrex.Wire;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace ApocoCrypto.MarketData.Bittrex.Api
{
    public interface IBittrexClient
    {
        Task<IReadOnlyCollection<Market>> GetMarkets();
        Task<IReadOnlyCollection<CurrencyResponse>> GetCurrencies();
        Task<Quote> GetTicker(string marketName);
        Task<IReadOnlyCollection<MarketSummary>> GetMarketSummaries();
        Task<MarketSummary> GetMarketSummary(string marketName);
        Task<FullOrderBook> GetFullOrderBook(string marketName);
        Task<IReadOnlyCollection<OrderBookEntry>> GetOrderBook(string marketName, string type);
        Task<IReadOnlyCollection<MarketHistory>> GetMarketHistory(string marketName);
        Task<IReadOnlyCollection<Tick>> GetTicks(string marketName, string tickInterval);
        Task<IReadOnlyCollection<T>> GetList<T>(string url, IDictionary<string, string> query = null);
        Task<IReadOnlyCollection<T>> GetList<T>(string baseUrl, string url, IDictionary<string, string> query = null);
        Task<T> GetSingle<T>(string url, IDictionary<string, string> query = null);
        Task<T> Get<T>(string baseUrl, string url, IDictionary<string, string> query = null);
    }

    public class BittrexClient : IBittrexClient
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

        public async Task<IReadOnlyCollection<Market>> GetMarkets()
        {
            return await GetList<Market>("public/getmarkets");
        }

        public async Task<IReadOnlyCollection<CurrencyResponse>> GetCurrencies()
        {
            return await GetList<CurrencyResponse>("public/getcurrencies");
        }

        public async Task<Quote> GetTicker(string marketName)
        {
            return await GetSingle<Quote>("public/getticker", new Dictionary<string, string> {{"market", marketName } });
        }

        public async Task<IReadOnlyCollection<MarketSummary>> GetMarketSummaries()
        {
            return await GetList<MarketSummary>("public/getmarketsummaries");
        }

        public async Task<MarketSummary> GetMarketSummary(string marketName)
        {
            var result = await GetList<MarketSummary>("public/getmarketsummary", new Dictionary<string, string> { { "market", marketName } });
            return result.FirstOrDefault();
        }

        public async Task<FullOrderBook> GetFullOrderBook(string marketName)
        {
            return await GetSingle<FullOrderBook>("public/getorderbook", new Dictionary<string, string> { { "market", marketName }, { "type", "both" } });
        }

        public async Task<IReadOnlyCollection<OrderBookEntry>> GetOrderBook(string marketName, string type)
        {
            return await GetList<OrderBookEntry>("public/getorderbook", new Dictionary<string, string> { { "market", marketName }, { "type", type } });
        }

        public async Task<IReadOnlyCollection<MarketHistory>> GetMarketHistory(string marketName)
        {
            return await GetList<MarketHistory>("public/getmarkethistory", new Dictionary<string, string> { { "market", marketName } });
        }

        public async Task<IReadOnlyCollection<Tick>> GetTicks(string marketName, string tickInterval)
        {
            return await GetList<Tick>(Apiv2, "pub/market/GetTicks", new Dictionary<string, string> { { "marketName", marketName }, { "tickInterval", tickInterval } });
        }

        public async Task<IReadOnlyCollection<T>> GetList<T>(string url, IDictionary<string, string> query = null)
        {
            var result = await Get<BittrexList<T>>(Apiv1, url, query);
            if (result.Success)
            {
                return result.Result;
            }

            throw new Exception($"Bittrex API failed: {result.Message}");
        }

        public async Task<IReadOnlyCollection<T>> GetList<T>(string baseUrl, string url, IDictionary<string, string> query = null)
        {
            var result = await Get<BittrexList<T>>(baseUrl, url, query);
            if (result.Success)
            {
                return result.Result;
            }

            throw new Exception($"Bittrex API failed: {result.Message}");
        }

        public async Task<T> GetSingle<T>(string url, IDictionary<string, string> query = null)
        {
            var result = await Get<BittrexSingle<T>>(Apiv1, url, query);
            if (result.Success)
            {
                return result.Result;
            }

            throw new Exception($"Bittrex API failed: {result.Message}");
        }

        public async Task<T> Get<T>(string baseUrl, string url, IDictionary<string, string> query = null)
        {
            if (query != null)
            {
                url = QueryHelpers.AddQueryString(url, query);
            }

            var response = await Client.GetAsync(baseUrl + url);

            var json = await response.Content.ReadAsStringAsync();

            var settings = new JsonSerializerSettings();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}