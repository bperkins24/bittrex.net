namespace ApocoCrypto.MarketData
{
    public interface IMarketSummary
    {
        string MarketName { get; }

        decimal BaseVolume { get; }
    }
}