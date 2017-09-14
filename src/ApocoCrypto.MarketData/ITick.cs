using System;

namespace ApocoCrypto.MarketData
{
    public interface ITick
    {
        DateTime Time { get; }

        decimal BaseVolume { get; }
    }
}