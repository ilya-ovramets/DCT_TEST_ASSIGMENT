using DCT_TEST_ASSIGNMENT.VIew;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Windows.Input;

namespace DCT_TEST_ASSIGNMENT.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private object _curentValue;
        
        public object CurrentValue
        {
            get => _curentValue;
            set
            {
                _curentValue = value;
                OnPropertyChanged(nameof(CurrentValue));
            }
        }

        public ICommand ShowHomePage { get; }
        public ICommand ShowConverterPage { get; }

        public ICommand ShowDetailsPage { get; }


        public MainViewModel() 
        {
            CurrentValue = new Home();

            ShowHomePage = new RelayCommand(o => CurrentValue= new Home());

            ShowConverterPage = new RelayCommand(o => CurrentValue= new Converter());

            ShowDetailsPage = new RelayCommand(o => CurrentValue = new Details());

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
