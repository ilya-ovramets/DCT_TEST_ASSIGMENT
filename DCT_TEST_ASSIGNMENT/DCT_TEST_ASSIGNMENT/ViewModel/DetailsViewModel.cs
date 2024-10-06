using DCT_TEST_ASSIGNMENT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DCT_TEST_ASSIGNMENT.ViewModel
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;

        private CryptoMainModel _selectedCurrency;

        private List<CryptoMainModel> _CryptoColection;

        private readonly APIWorkingModel _APIWorkingModel;

        public CryptoMainModel SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }

        public ICommand SearchCommand { get; }

        public DetailsViewModel()
        {
            _APIWorkingModel = new APIWorkingModel();

            SearchCommand = new RelayCommand(Search);

            GetCrypto();
        }

        private async void GetCrypto()
        {
            _CryptoColection = await _APIWorkingModel.GetTopCryptoCurrenciesAsync();
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
                FilterCryptoCurrencies();
            }
        }

        private void Search(object parametr)
        {
            FilterCryptoCurrencies();
        }

        

        private async void FilterCryptoCurrencies()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                MessageBox.Show("Plase write some text", "Title", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var filtered = _CryptoColection.Where(c => c.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                c.CoinId.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

                if (filtered.Count == 0) 
                {
                    MessageBox.Show("No matching cryptocurrencies found.", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                SelectedCurrency = filtered.FirstOrDefault(); ;
                
            }

        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
