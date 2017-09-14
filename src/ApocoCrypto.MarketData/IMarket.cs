namespace ApocoCrypto.MarketData
{
    public interface IMarket
    {
        string Name { get; }

        string BaseCurrency { get; }

        string MarketCurrency { get; }
    }
}