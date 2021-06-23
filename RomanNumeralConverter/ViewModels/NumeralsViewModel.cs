using System;
using System.Collections.Generic;
using System.ComponentModel;
using RomanNumeralConverter.Model;

namespace RomanNumeralConverter.ViewModels
{
    public class NumeralsViewModel : INotifyPropertyChanged
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
                Roman = ArabicToRoman(numeralsModel.Arabic);
                OnPropertyChanged(nameof(Arabic));
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
                numeralsModel.Roman = value;
                OnPropertyChanged(nameof(Roman));
            }
        }

        private string ArabicToRoman(long num)
        {
            
            var result = new List<string>();
            var ValToSymbol = new Dictionary<int, string>
            {
                { 1000, "M" },
                { 900, "CM" },
                { 500, "D" },
                { 400, "CD" },
                { 100, "C" },
                { 90, "XC" },
                { 50, "L" },
                { 40, "XL" },
                { 10, "X" },
                { 9, "IX" },
                { 5, "V" },
                { 4, "IV" },
                { 1, "I" },
            };

            foreach (var pair in ValToSymbol)
            {
                while (num >= pair.Key)
                {
                    result.Add(pair.Value);
                    num -= pair.Key;
                }
            }

            return String.Concat(result);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
