using System.Collections.Generic;

namespace ApocoCrypto.Bittrex.Client.Response
{
    public class BittrexList<T> : BittrexResponse
    {
        public IReadOnlyCollection<T> Result { get; set; }
    }
}