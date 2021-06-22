using System;
using System.ComponentModel;
using RomanNumeralConverter.Model;

namespace RomanNumeralConverter.ViewModels
{
    public class NumeralsViewModel
    {
        private NumeralsModel numeralsModel;
        public NumeralsViewModel()
        {
            numeralsModel = new NumeralsModel();
        }
     
        public string Arabic
        {
            get
            {
                return (numeralsModel.Arabic != 0)? numeralsModel.Arabic.ToString() : "";
            }

            set 
            {
                numeralsModel.Arabic = (value != "") ? Int64.Parse(value) : 0;
                OnPropertyChanged("Arabic");
            }
        }

        public string Roman
        {
            get 
            { 
                return numeralsModel.Roman; 
            }

            set 
            { 
                numeralsModel.Roman = value; OnPropertyChanged("Roman"); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
