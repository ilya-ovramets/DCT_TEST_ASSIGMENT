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

        public HomeViewModel()
        {
            _APIWorkingModel = new APIWorkingModel();
            CryptoCurrencies = new ObservableCollection<CryptoMainModel>();
            LoadData();
        }

        private async void LoadData()
        {
            var currencies = await _APIWorkingModel.GetTopCryptoCurrenciesAsync();
            foreach (var currency in currencies)
            {
                CryptoCurrencies.Add(currency);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
