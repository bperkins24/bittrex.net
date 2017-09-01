using System.Collections.Generic;

namespace ApocoCrypto.Bittrex.Client.Response
{
    public class FullOrderBook
    {
        public IReadOnlyCollection<OrderBookEntry> Buy { get; set; }

        public IReadOnlyCollection<OrderBookEntry> Sell { get; set; }
    }
}