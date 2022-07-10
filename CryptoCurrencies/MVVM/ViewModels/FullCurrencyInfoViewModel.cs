using CryptoCurrencies.Infrastructure;
using CryptoCurrencies.MVVM.Models;
using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
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
    
    public class FullCurrencyInfoViewModel : BaseNotify
    {
        private NavigationViewModel navigation;

        public NavigationViewModel Navigation
        {
            get { return navigation; }
            set { navigation = value; Notify(); }
        }




        private PlotModel plotModel;
        public PlotModel PlotModel { get => plotModel; set { plotModel = value; Notify(); } }

        public ObservableCollection<DataPoint> Points{ get; set; }

        private void DesinPlot(PlotModel plotModel)
        {
            plotModel.SubtitleColor = OxyColors.Black;
            plotModel.PlotAreaBorderColor = OxyColors.Black;
            plotModel.TextColor = OxyColors.Black;
            
        }


        private void GetStory(CurrencyInfo currencyInfo)
        {

            WebClient wc = new WebClient();
            string str = wc.DownloadString("https://" + $"api.coincap.io/v2/assets/{currencyInfo.Name.ToLower().Replace(' ','-')}/history?interval=d1");
            JObject jo = JObject.Parse(str);

            for (int i = 0; i < jo["data"].Count(); i++)
            {
                long unixDate = (long)jo["data"][i]["time"];
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime date = start.AddMilliseconds(unixDate).ToLocalTime();

                Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), (double)jo["data"][i]["priceUsd"]));
            }

            PlotModel.Series.Add(new LineSeries
            {
                Color = OxyColors.Green,
                StrokeThickness = 2,
                MarkerSize = 3,
                ItemsSource = Points
            });

            this.PlotModel.Title = currencyInfo.Name;
            var dateAxis = new DateTimeAxis()
            {
                MajorGridlineStyle = LineStyle.DashDashDot,
                MinorGridlineStyle = LineStyle.Dot,
                IntervalLength = 100,
                FontSize = 18,
                TextColor = OxyColors.Black,
                Title = "Date"
            };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Dash,
                MinorGridlineStyle = LineStyle.Dot,
                Title = "Price",
                FontSize = 18,
                TextColor = OxyColors.Black,
                IntervalLength = 100
            };
            PlotModel.Axes.Add(valueAxis);
        }

        private ICommand backCommand;

        public ICommand BackCommand
        {
            get { return backCommand; }
            set { backCommand = value; Notify(); }
        }

        public string CurrencyImage { get; set; }

        public FullCurrencyInfoViewModel(NavigationViewModel navigation, CurrencyInfo currencyInfo)
        {
            CurrencyImage = currencyInfo.ImageURL;

            BackCommand = new RelayCommand(x =>
            {
                navigation.SelectedViewModel = new TopCurrenciesViewModel(navigation);
            });

            Points = new ObservableCollection<DataPoint>();

            PlotModel = new PlotModel();
            DesinPlot(PlotModel);
            GetStory(currencyInfo);


            

            this.Navigation = navigation;
        }


    }
}
