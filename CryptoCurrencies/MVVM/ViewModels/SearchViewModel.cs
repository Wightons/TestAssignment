using CryptoCurrencies.Infrastructure;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CryptoCurrencies.MVVM.ViewModels
{
    public class SearchViewModel : BaseNotify
    {
        private NavigationViewModel navigation;

        public NavigationViewModel Navigation
        {
            get { return navigation; }
            set { navigation = value; Notify(); }
        }

        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set { searchText = value; Notify(); }
        }

        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get { return searchCommand; }
            set { searchCommand = value; Notify(); }
        }

        private ICommand selectionCommand;

        public ICommand SelectionCommand
        {
            get { return selectionCommand; }
            set { selectionCommand = value; Notify(); }
        }

        private string imageUrl = "https://viridis.ua/templates/pub/default/resources/images/no_image_new_1.jpg";

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; Notify(); }
        }

        private string currencyName;

        public string CurrencyName
        {
            get { return currencyName; }
            set { currencyName = value; Notify(); }
        }

        private ICommand backCommand;

        public ICommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; Notify(); }
        }


        public SearchViewModel(NavigationViewModel navigation)
        {

            BackCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new TopCurrenciesViewModel(navigation);
            });

            SelectionCommand = new RelayCommand(x =>
            {

                try
                {

                    string url = "https://" + $"api.coincap.io/v2/assets/{SearchText.ToLower()}";
                    WebClient wc = new WebClient();
                    string str = wc.DownloadString(url);
                    JObject jo = JObject.Parse(str);

                    MessageBox.Show($"Rank: {jo["data"]["rank"].ToString()}\n" +
                                    $"Symbol: {jo["data"]["symbol"].ToString()}\n" +
                                    $"Price Usd: {jo["data"]["priceUsd"].ToString()}");
                
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            });

            SearchCommand = new RelayCommand(x =>
            {
                try
                {

                    if (!String.IsNullOrEmpty(SearchText))
                    {
                        string url = "https://" + $"api.coincap.io/v2/assets/{SearchText.ToLower()}";
                        WebClient wc = new WebClient();
                        string str = wc.DownloadString(url);
                        JObject jo = JObject.Parse(str);
                        ImageUrl = "https://assets.coincap.io/assets/icons/" + jo["data"]["symbol"].ToString().ToLower() + "@2x.png";
                        CurrencyName = jo["data"]["name"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            });


            this.Navigation = navigation;
        }

    }
}
