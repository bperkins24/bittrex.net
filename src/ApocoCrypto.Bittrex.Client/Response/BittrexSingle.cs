namespace ApocoCrypto.Bittrex.Client.Response
{
    public class BittrexSingle<T> : BittrexResponse
    {
        public T Result { get; set; }
    }
}