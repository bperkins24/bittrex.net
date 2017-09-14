using System;

namespace ApocoCrypto.MarketData
{
    public static class TickExtensions
    {
        public static bool IsInIntervalWindow(this ITick t, string tickInterval, double intervalWindow)
        {
            if (tickInterval == "day")
                return t.Time >= DateTime.Today.AddDays(-intervalWindow);

            return false;
        }
    }
}