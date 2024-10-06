using DCT_TEST_ASSIGNMENT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT_TEST_ASSIGNMENT.ViewModel
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private readonly APIWorkingModel _APIWorkingModel;

        public ObservableCollection<CryptoMainModel> CryptoCurrencies { get; private set; }

        private int N = 10;
        private int accuracy = 4;


        public HomeViewModel()
        {
            _APIWorkingModel = new APIWorkingModel();
            CryptoCurrencies = new ObservableCollection<CryptoMainModel>();
            LoadData();
        }

        private async void LoadData()
        {
            var currencies = await _APIWorkingModel.GetTopCryptoCurrenciesAsync();

            int i = 0;
            foreach (var currency in currencies)
            {
                i++;
                currency.Price = Math.Round(currency.Price,accuracy);
                currency.CoinCapitalization = Math.Round(currency.CoinCapitalization, 1);
                CryptoCurrencies.Add(currency);
                if (i > N) 
                {
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
