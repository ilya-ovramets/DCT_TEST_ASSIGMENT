using DCT_TEST_ASSIGNMENT.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DCT_TEST_ASSIGNMENT.ViewModel
{
    class ConverterViewModel : INotifyPropertyChanged
    {
        private string amount;
        private CryptoMainModel fromCurrency;
        private CryptoMainModel toCurrency;
        private decimal conversionResult;
        private List<CryptoMainModel> _cryptosCurrencies;


        public event PropertyChangedEventHandler PropertyChanged;

        public List<CryptoMainModel> CryptoCurrencies { 
           get { return _cryptosCurrencies; }
            set {
                _cryptosCurrencies = value;
                OnPropertyChanged(nameof(CryptoCurrencies));
            } 
        }
        private readonly APIWorkingModel _APIWorkingModel;


        public string Amount
        {
            get { return amount; }
            set
            {
                amount = value.Replace(".",",");
                OnPropertyChanged();
            }
        }

        public CryptoMainModel FromCurrency
        {
            get { return fromCurrency; }
            set
            {
                fromCurrency = value;
                OnPropertyChanged();
            }
        }

        public CryptoMainModel ToCurrency
        {
            get { return toCurrency; }
            set
            {
                toCurrency = value;
                OnPropertyChanged();
            }
        }

        public decimal ConversionResult
        {
            get { return conversionResult; }
            set
            {
                conversionResult = value;
                OnPropertyChanged(nameof(ConversionResult));
            }
        }

        public ICommand ConvertCommand { get; }

        public ConverterViewModel()
        {
            _APIWorkingModel = new APIWorkingModel();
            GetCrypto();
            ConvertCommand = new RelayCommand(ConvertCurrency);
        }


        private async void GetCrypto()
        {
            try
            {
                CryptoCurrencies = await _APIWorkingModel.GetTopCryptoCurrenciesAsync();
                

            }
            catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConvertCurrency(object parametr)
        {

            if (FromCurrency == null || ToCurrency == null)
            {
                MessageBox.Show("Please select both currencies.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CryptoMainModel from = FromCurrency;
            CryptoMainModel to = ToCurrency;

            if (decimal.TryParse(amount, out decimal rAmount))
            {
                decimal fromInUSD = rAmount * fromCurrency.Price;

                ConversionResult = fromInUSD / toCurrency.Price;

            }
            else
            {
                MessageBox.Show("Please enter a valid number for the amount.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
