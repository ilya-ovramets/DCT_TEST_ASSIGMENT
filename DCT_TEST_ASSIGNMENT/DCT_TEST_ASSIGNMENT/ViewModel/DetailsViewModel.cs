using DCT_TEST_ASSIGNMENT.Model;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DCT_TEST_ASSIGNMENT.ViewModel
{
    class DetailsViewModel : INotifyPropertyChanged
    {
        private string _searchQuery;

        private List<CryptoMainModel> _SearchSuggestions;

        private List<CryptoMainModel> _CryptoColection;

        private readonly APIWorkingModel _APIWorkingModel;


        //Selected item
        private CryptoMainModel _selectedCurrency;
        public CryptoMainModel SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged(nameof(SelectedCurrency));
            }
        }


        public List<CryptoMainModel> SearchSuggestions
        {
            get => _SearchSuggestions;
            set
            {
                _SearchSuggestions = value;
                OnPropertyChanged(nameof(SearchSuggestions));
            }
        }



        public ICommand SearchCommand { get; }

        public ICommand OpenSiteCommand { get; }


        public DetailsViewModel()
        {
            _APIWorkingModel = new APIWorkingModel();

            SearchCommand = new RelayCommand(Search);

            OpenSiteCommand = new RelayCommand(OpenSite);

            GetCrypto();
        }


        private void OpenSite(object paramtr)
        {
            try
            {
                Process.Start(new ProcessStartInfo(_selectedCurrency.Explorer) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));
                    FilterCryptoCurrencies();
                }
            }
        }

        private void Search(object parametr)
        {
            
        }

        

        private void FilterCryptoCurrencies()
        {
            if (string.IsNullOrEmpty(SearchQuery)) { }
            else
            {
                var filtered = _CryptoColection.Where(c => c.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                c.CoinId.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)).ToList();

                SearchSuggestions = filtered;
                
            }

        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
