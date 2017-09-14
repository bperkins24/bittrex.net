using System;

namespace ApocoCrypto.CoinGuard.Wpf.Views
{
    public partial class ShellView
    {
        private void OnItemsSourceChangeCompleted(object sender, EventArgs e)
        {
            Grid.Columns["PercentChange"].CellContentStringFormat = "{0:P2}";
        }
    }
}