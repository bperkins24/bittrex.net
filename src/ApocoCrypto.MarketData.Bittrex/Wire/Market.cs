using System;

namespace ApocoCrypto.MarketData.Bittrex.Wire
{
    public class Market : IMarket
    {
        string IMarket.Name => MarketName;

        public string MarketName { get; set; }

        public string MarketCurrency { get; set; }

        public string BaseCurrency { get; set; }

        public string MarketCurrencyLong { get; set; }

        public string BaseCurrencyLong { get; set; }

        public decimal MinTradeSize { get; set; }

        public bool IsActive { get; set; }

        public DateTime Created { get; set; }
    }
}