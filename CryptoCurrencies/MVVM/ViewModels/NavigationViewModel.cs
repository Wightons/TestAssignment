using CryptoCurrencies.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrencies.MVVM.ViewModels
{
    public class NavigationViewModel : BaseNotify
    {
        private object selectedViewModel;

        public object SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }
            set
            {
                selectedViewModel = value;
                Notify();
            }
        }
    }
}
