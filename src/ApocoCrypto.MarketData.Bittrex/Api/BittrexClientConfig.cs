using System;
using System.Net.Http;

namespace ApocoCrypto.MarketData.Bittrex.Api
{
    public class BittrexClientConfig
    {
        private const string UrlDefault = "https://bittrex.com/";

        public static readonly BittrexClientConfig Default = new BittrexClientConfig(UrlDefault);

        public Uri Url { get; set; }

        public HttpClientHandler Handler { get; set; }

        public BittrexClientConfig()
        {
        }

        public BittrexClientConfig(string url, HttpClientHandler handler = null)
        {
            Url = new Uri(url);
            Handler = handler;
        }
    }
}