using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.MVVM.Models
{
    public class CurrencyInfo
    {
        public string Rank { get; set; }
        public string ImageURL { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal PriceUSD { get; set; }
        public decimal MarketCapUsd { get; set; }
        public decimal VWAP24Hr { get; set; }
        public decimal Supply { get; set; }
        public decimal VolumeUsd24Hr { get; set; }
        public decimal ChangePercent24Hr { get; set; }
    }
}
