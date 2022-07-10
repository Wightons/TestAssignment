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
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrencies.MVVM.ViewModels
{
    internal class CalculatorViewModel : BaseNotify
    {

        private NavigationViewModel navigation;

        public NavigationViewModel Navigation
        {
            get { return navigation; }
            set { navigation = value; Notify(); }
        }

        private ICommand backCommand;

        public ICommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; Notify(); }
        }

        private string sellCurrency;

        public string SellCurrency
        {
            get { return sellCurrency; }
            set { sellCurrency = value; Notify(); }
        }

        private string getCurrency;

        public string GetCurrency
        {
            get { return getCurrency; }
            set { getCurrency = value; Notify(); }
        }

        private ObservableCollection<InfoForExchange> collectionOfCurrencies;

        public ObservableCollection<InfoForExchange> CollectionOfCurrencies
        {
            get { return collectionOfCurrencies; }
            set { collectionOfCurrencies = value; Notify(); }
        }


        public void GetAssets()
        {

            WebClient wc = new WebClient();
            string res = wc.DownloadString("https://api.coincap.io/v2/assets?limit=100");
            JObject jo = JObject.Parse(res);

            CollectionOfCurrencies = new ObservableCollection<InfoForExchange>();

            foreach (var item in jo["data"].ToList())
            {
                CollectionOfCurrencies.Add(new InfoForExchange
                {
                    Name = (string)item["name"],
                    PriceUSD = String.IsNullOrEmpty(item["priceUsd"].ToString()) ? 0 : Math.Round(Convert.ToDecimal(item["priceUsd"]), 5)
                });
            }
        }

        private ICommand calculateCommand;

        public ICommand CalculateCommand
        {
            get { return calculateCommand; }
            set { calculateCommand = value; Notify(); }
        }

        private InfoForExchange selectedCurrencyToSell;

        public InfoForExchange SelectedCurrencyToSell
        {
            get { return selectedCurrencyToSell; }
            set { selectedCurrencyToSell = value; Notify(); }
        }

        private InfoForExchange selectedCurrencyToGet;

        public InfoForExchange SelectedCurrencyToGet
        {
            get { return selectedCurrencyToGet; }
            set { selectedCurrencyToGet = value; Notify(); }
        }


        public CalculatorViewModel(NavigationViewModel navigation)
        {
            GetAssets();

            CalculateCommand = new RelayCommand(x =>
            {
                try
                {
                    double r = Convert.ToDouble(SelectedCurrencyToSell.PriceUSD) * Convert.ToDouble(SellCurrency);
                    double d = r / Convert.ToDouble(SelectedCurrencyToGet.PriceUSD);
                    GetCurrency = d.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            });

            BackCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new TopCurrenciesViewModel(navigation);
            });


            this.Navigation = navigation;
        }

    }
}
