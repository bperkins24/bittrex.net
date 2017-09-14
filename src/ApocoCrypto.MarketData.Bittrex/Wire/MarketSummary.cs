using System;

namespace ApocoCrypto.MarketData.Bittrex.Wire
{
    public class MarketSummary : IMarketSummary
    {
        public string MarketName { get; set; }

        public decimal Volume { get; set; }

        public decimal BaseVolume { get; set; }

        public decimal PrevDay { get; set; }

        public decimal Last { get; set; }

        public decimal Bid { get; set; }

        public decimal Ask { get; set; }

        public decimal High { get; set; }

        public decimal Low { get; set; }

        public int OpenBuyOrders { get; set; }

        public int OpenSellOrders { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime Created { get; set; }

        protected bool Equals(MarketSummary other)
        {
            return string.Equals(MarketName, other.MarketName);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MarketSummary) obj);
        }

        public override int GetHashCode()
        {
            return MarketName != null ? MarketName.GetHashCode() : 0;
        }
    }
}