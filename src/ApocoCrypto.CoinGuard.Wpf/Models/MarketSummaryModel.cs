using System;
using System.ComponentModel;
using Caliburn.Micro;

namespace ApocoCrypto.CoinGuard.Wpf.Models
{
    public class MarketSummaryModel : PropertyChangedBase
    {
        private string _marketName;
        private int _openSellOrders;
        private int _openBuyOrders;
        private decimal _low;
        private decimal _high;
        private decimal _ask;
        private decimal _bid;
        private decimal _last;
        private decimal _prevDay;
        private decimal _baseVolume;
        private decimal _volume;
        private DateTime _timeStamp;
        private DateTime _created;

        public string MarketName
        {
            get => _marketName;
            set
            {
                if (value == _marketName) return;
                _marketName = value;
                NotifyOfPropertyChange(() => MarketName);
            }
        }

        public decimal Volume
        {
            get => _volume;
            set
            {
                if (value == _volume) return;
                _volume = value;
                NotifyOfPropertyChange(() => Volume);
            }
        }

        public decimal BaseVolume
        {
            get => _baseVolume;
            set
            {
                if (value == _baseVolume) return;
                _baseVolume = value;
                NotifyOfPropertyChange(() => BaseVolume);
            }
        }

        public decimal PrevDay
        {
            get => _prevDay;
            set
            {
                if (value == _prevDay) return;
                _prevDay = value;
                NotifyOfPropertyChange(() => PrevDay);
                NotifyOfPropertyChange(() => Change);
                NotifyOfPropertyChange(() => PercentChange);
            }
        }

        public decimal Last
        {
            get => _last;
            set
            {
                if (value == _last) return;
                _last = value;
                NotifyOfPropertyChange(() => Last);
                NotifyOfPropertyChange(() => Change);
                NotifyOfPropertyChange(() => PercentChange);
            }
        }

        public decimal Change => Last - PrevDay;

        public decimal PercentChange => (Last - PrevDay) / PrevDay;

        public decimal Bid
        {
            get => _bid;
            set
            {
                if (value == _bid) return;
                _bid = value;
                NotifyOfPropertyChange(() => Bid);
            }
        }

        public decimal Ask
        {
            get => _ask;
            set
            {
                if (value == _ask) return;
                _ask = value;
                NotifyOfPropertyChange(() => Ask);
            }
        }

        public decimal High
        {
            get => _high;
            set
            {
                if (value == _high) return;
                _high = value;
                NotifyOfPropertyChange(() => High);
            }
        }

        public decimal Low
        {
            get => _low;
            set
            {
                if (value == _low) return;
                _low = value;
                NotifyOfPropertyChange(() => Low);
            }
        }

        public int OpenBuyOrders
        {
            get => _openBuyOrders;
            set
            {
                if (value == _openBuyOrders) return;
                _openBuyOrders = value;
                NotifyOfPropertyChange(() => OpenBuyOrders);
            }
        }

        public int OpenSellOrders
        {
            get => _openSellOrders;
            set
            {
                if (value == _openSellOrders) return;
                _openSellOrders = value;
                NotifyOfPropertyChange(() => OpenSellOrders);
            }
        }

        [Browsable(false)]
        public DateTime TimeStamp
        {
            get => _timeStamp;
            set
            {
                if (value.Equals(_timeStamp)) return;
                _timeStamp = value;
                NotifyOfPropertyChange(() => TimeStamp);
                NotifyOfPropertyChange(() => TimeStampLocal);
            }
        }

        public DateTime TimeStampLocal => TimeStamp.ToLocalTime();

        [Browsable(false)]
        public DateTime Created
        {
            get => _created;
            set
            {
                if (value.Equals(_created)) return;
                _created = value;
                NotifyOfPropertyChange(() => Created);
                NotifyOfPropertyChange(() => CreatedLocal);
            }
        }

        public DateTime CreatedLocal => Created.ToLocalTime();
    }
}