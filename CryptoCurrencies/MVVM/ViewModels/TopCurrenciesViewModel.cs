using CryptoCurrencies.Infrastructure;
using CryptoCurrencies.MVVM.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CryptoCurrencies.MVVM.ViewModels
{
    public class TopCurrenciesViewModel : BaseNotify
    {

        private NavigationViewModel navigation;

        public NavigationViewModel Navigation
        {
            get
            {
                return navigation;
            }
            set
            {
                navigation = value;
                Notify();
            }
        }

        private ObservableCollection<CurrencyInfo> collectionOfCurrencies;

        public ObservableCollection<CurrencyInfo> CollectionOfCurrencies
        {
            get { return collectionOfCurrencies; }
            set { collectionOfCurrencies = value; Notify(); }
        }

        private int numberOfCurrencies = 10;

        public int NumberOfCurrencies
        {
            get { return numberOfCurrencies; }
            set { numberOfCurrencies = value; Notify(); }
        }

        private CurrencyInfo selectedCurrency;

        public CurrencyInfo SelectedCurrency
        {
            get { return selectedCurrency; }
            set { selectedCurrency = value; Notify(); }
        }



        public void GetAssets()
        {

            WebClient wc = new WebClient();
            string res = wc.DownloadString("https://" + $"api.coincap.io/v2/assets?limit={NumberOfCurrencies}");
            JObject jo = JObject.Parse(res);

            CollectionOfCurrencies = new ObservableCollection<CurrencyInfo>();

            foreach (var item in jo["data"].ToList())
            {

                CollectionOfCurrencies.Add(new CurrencyInfo
                {
                    Rank = (string)item["rank"],
                    ImageURL = "https://assets.coincap.io/assets/icons/" + item["symbol"].ToString().ToLower() + "@2x.png",
                    Name = (string)item["name"],
                    Symbol = (string)item["symbol"],
                    PriceUSD = String.IsNullOrEmpty(item["priceUsd"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["priceUsd"]), 5),
                    MarketCapUsd = String.IsNullOrEmpty(item["marketCapUsd"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["marketCapUsd"]), 2),
                    VWAP24Hr = String.IsNullOrEmpty(item["vwap24Hr"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["vwap24Hr"]), 2),
                    Supply = String.IsNullOrEmpty(item["supply"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["supply"]), 2),
                    VolumeUsd24Hr = String.IsNullOrEmpty(item["volumeUsd24Hr"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["volumeUsd24Hr"]), 2),
                    ChangePercent24Hr = String.IsNullOrEmpty(item["changePercent24Hr"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["changePercent24Hr"]), 2)

                });
            }
        }

        private ICommand refreshCommand;
        public ICommand RefreshCommand
        {
            get => refreshCommand;
            set
            {
                refreshCommand = value;
                Notify();
            }
        }

        private ICommand selectionCommand;

        public ICommand SelectionCommand
        {
            get { return selectionCommand; }
            set { selectionCommand = value; Notify(); }
        }

        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get { return  searchCommand; }
            set {  searchCommand = value; Notify(); }
        }

        private ICommand calculateCommand;

        public ICommand CalculateCommand
        {
            get { return calculateCommand; }
            set { calculateCommand = value; Notify(); }
        }


        public TopCurrenciesViewModel(NavigationViewModel navigation)
        {

            GetAssets();

            RefreshCommand = new RelayCommand(x =>
            {
                GetAssets();
            });

            SelectionCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new FullCurrencyInfoViewModel(navigation, SelectedCurrency);
            });

            CalculateCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new CalculatorViewModel(navigation);
            });

            SearchCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new SearchViewModel(navigation);
            });

            this.navigation = navigation;

        }

    }
}
