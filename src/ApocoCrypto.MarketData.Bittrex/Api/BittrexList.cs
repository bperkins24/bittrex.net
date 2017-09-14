using System.Collections.Generic;

namespace ApocoCrypto.MarketData.Bittrex.Api
{
    public class BittrexList<T> : BittrexResponse
    {
        public IReadOnlyCollection<T> Result { get; set; }
    }
}