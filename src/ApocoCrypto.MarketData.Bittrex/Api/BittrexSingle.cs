namespace ApocoCrypto.MarketData.Bittrex.Api
{
    public class BittrexSingle<T> : BittrexResponse
    {
        public T Result { get; set; }
    }
}