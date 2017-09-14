using System.Collections.Generic;

namespace ApocoCrypto.MarketData.Bittrex.Wire
{
    public class FullOrderBook
    {
        public IReadOnlyCollection<OrderBookEntry> Buy { get; set; }

        public IReadOnlyCollection<OrderBookEntry> Sell { get; set; }
    }
}