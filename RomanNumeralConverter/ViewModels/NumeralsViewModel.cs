using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using RomanNumeralConverter.Models;

namespace RomanNumeralConverter.ViewModels
{
    public class NumeralsViewModel : INotifyPropertyChanged
    {
        private readonly NumeralsModel _numeralsModel;
        private readonly Regex _arabicRegex = new Regex(@"^[0-4]?\d{0,3}$");
        private readonly Regex _romanRegex = new Regex(@"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
        
        public string Arabic
        {
            get => (_numeralsModel.Arabic != 0)? _numeralsModel.Arabic.ToString() : "";

            set 
            {
                if (!_arabicRegex.IsMatch(value))
                {
                    return;
                }

                _numeralsModel.Arabic = (value != "") ? int.Parse(value) : 0;
                _numeralsModel.Roman = ArabicToRoman(_numeralsModel.Arabic);
                OnPropertyChanged(nameof(Roman));
                OnPropertyChanged(nameof(Arabic));
            }
        }
        
        public string Roman
        {
            get => _numeralsModel.Roman;

            set 
            {
                if (!_romanRegex.IsMatch(value.ToUpper()))
                {
                    return;
                }

                _numeralsModel.Roman = value.ToUpper();
                _numeralsModel.Arabic = RomanToArabic(_numeralsModel.Roman);
                OnPropertyChanged(nameof(Arabic));
                OnPropertyChanged(nameof(Roman));
            }
        }
        
        public NumeralsViewModel()
        {
            _numeralsModel = new NumeralsModel();
        }
        
        private static string ArabicToRoman(long num)
        {
            var result = new List<string>();
            var symbolToVal = new Dictionary<string, int>
            {
                { "M" , 1000 },
                { "CM", 900 },
                { "D" , 500 },
                { "CD", 400 },
                { "C" , 100 },
                { "XC", 90 },
                { "L" , 50 },
                { "XL", 40 },
                { "X" , 10 },
                { "IX", 9 },
                { "V" , 5 },
                { "IV", 4 },
                { "I" , 1 },
            };

            foreach (var (symbol, value) in symbolToVal)
            {
                while (num >= value)
                {
                    result.Add(symbol);
                    num -= value;
                }
            }

            return string.Concat(result);
        }
        
        private static int RomanToArabic(string letter)
        {
            var result = 0;
            var symbolToVal = new Dictionary<char, int>
            {
                { 'M' , 1000 },
                { 'D' , 500 },
                { 'C' , 100 },
                { 'L' , 50 },
                { 'X' , 10 },
                { 'V' , 5 },
                { 'I' , 1 },
            };

            for (var i=0; i < letter.Length; ++i)
            {
                // To handle subtractive notations such as CM
                if (i+1 < letter.Length && symbolToVal[letter[i]] < symbolToVal[letter[i+1]])
                {
                    result += symbolToVal[letter[i+1]] - symbolToVal[letter[i]];
                    ++i; // To skip next character
                    continue;
                }

                result += symbolToVal[letter[i]];
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
