using System.Threading.Tasks;
using Xunit;

namespace ApocoCrypto.Bittrex.Client.Tests
{
    public class BittrexClientTests
    {
        public BittrexClientTests()
        {
            _client = new BittrexClient(BittrexClientConfig.Default);
        }

        private readonly BittrexClient _client;

        [Fact]
        public async Task ShouldGetCurrencies()
        {
            var response = await _client.GetCurrencies();

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }

        [Fact]
        public async Task ShouldGetMarkets()
        {
            var response = await _client.GetMarkets();

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }

        [Fact]
        public async Task ShouldGetTicker()
        {
            var response = await _client.GetTicker("BTC-ETH");

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result != null);
        }

        [Fact]
        public async Task ShouldGetMarketSummaries()
        {
            var response = await _client.GetMarketSummaries();

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }

        [Fact]
        public async Task ShouldGetMarketSummary()
        {
            var response = await _client.GetMarketSummary("BTC-ETH");

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result != null);
        }

        [Fact]
        public async Task GetFullOrderBook()
        {
            var response = await _client.GetFullOrderBook("BTC-ETH");

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result != null);
            Assert.True(response.Result.Buy.Count > 0);
            Assert.True(response.Result.Sell.Count > 0);
        }

        [Theory]
        [InlineData("buy")]
        [InlineData("sell")]
        public async Task GetOrderBook(string type)
        {
            var response = await _client.GetOrderBook("BTC-ETH", type);

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }

        [Fact]
        public async Task GetMarketHistory()
        {
            var response = await _client.GetMarketHistory("BTC-ETH");

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }

        [Theory]
        [InlineData("thirtyMin")]
        [InlineData("oneHour")]
        public async Task GetTicks(string tickInterval)
        {
            var response = await _client.GetTicks("BTC-ETH", tickInterval);

            Assert.True(response.Success, response.Message);
            Assert.True(response.Result.Count > 0);
        }
    }
}