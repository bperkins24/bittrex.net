using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ApocoCrypto.CoinGuard.Wpf.Models;
using ApocoCrypto.MarketData.Bittrex.Api;
using ApocoCrypto.MarketData.Bittrex.Wire;
using AutoMapper;
using Caliburn.Micro;

namespace ApocoCrypto.CoinGuard.Wpf.ViewModels
{
    public class ShellViewModel : Screen, IShellViewModel, IDisposable
    {
        private readonly BittrexClient _client;

        private int _refreshInterval = 5;
        private Timer _timer;

        public int RefreshInterval
        {
            get => _refreshInterval;
            set
            {
                if (value == _refreshInterval) return;
                _refreshInterval = value;
                NotifyOfPropertyChange(() => RefreshInterval);
                _timer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(value));
            }
        }

        public BindableCollection<MarketSummaryModel> MarketSummaries { get; }

        public string RefreshTime => DateTime.Now.ToString("t");

        public ShellViewModel(BittrexClient client)
        {
            _client = client;
            MarketSummaries = new BindableCollection<MarketSummaryModel>();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _timer = new Timer(
                o =>
                {
                    _client.GetMarketSummaries().ContinueWith(c =>
                    {
                        Merge(c.Result);
                    });
                }, null, TimeSpan.Zero, TimeSpan.FromSeconds(RefreshInterval));
        }

        private void Merge(IReadOnlyCollection<MarketSummary> result)
        {
            var items = result.Where(r => r.MarketName.StartsWith("BTC"));

            Mapper.Map(items, MarketSummaries);

            //foreach (var r in result.Result)
            //{
            //    var existing = _marketSummaries.FirstOrDefault(m => Equals(m, r));
            //    if (existing == null)
            //    {
            //        _marketSummaries.Add(new MarketSummaryModel(r));
            //    }
            //    else
            //    {
                    
            //    }
            //}
            
            NotifyOfPropertyChange(() => RefreshTime);
        }

        //private void Merge(MarketSummary existing, MarketSummary r)
        //{
        //    throw new NotImplementedException();
        //}
    }
}