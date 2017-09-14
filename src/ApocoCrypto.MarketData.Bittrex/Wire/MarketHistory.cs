using System;

namespace ApocoCrypto.MarketData.Bittrex.Wire
{
    public class MarketHistory
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Total { get; set; }

        public string FillType { get; set; }

        public string OrderType { get; set; }
    }
}