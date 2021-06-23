﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
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

        Regex arabicRegex = new Regex(@"^[0-4]?\d{0,3}$");
        public string Arabic
        {
            get
            {
                return (numeralsModel.Arabic != 0)? numeralsModel.Arabic.ToString() : "";
            }

            set 
            {
                if (!arabicRegex.IsMatch(value))
                {
                    return;
                }

                numeralsModel.Arabic = (value != "") ? Int64.Parse(value) : 0;
                numeralsModel.Roman = ArabicToRoman(numeralsModel.Arabic);
                OnPropertyChanged(nameof(Roman));
                OnPropertyChanged(nameof(Arabic));
            }
        }

        Regex romanRegex = new Regex(@"^M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$");
        public string Roman
        {
            get 
            {
                return numeralsModel.Roman; 
            }

            set 
            {
                if (!romanRegex.IsMatch(value.ToUpper()))
                {
                    return;
                }

                numeralsModel.Roman = value.ToUpper();
                numeralsModel.Arabic = RomanToArabic(numeralsModel.Roman);
                OnPropertyChanged(nameof(Arabic));
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
        
        private long RomanToArabic(string letter)
        {
            long result = 0;
            var SymbolToVar = new Dictionary<char, int>
            {
                { 'M' , 1000 },
                { 'D' , 500 },
                { 'C' , 100 },
                { 'L' , 50 },
                { 'X' , 10 },
                { 'V' , 5 },
                { 'I' , 1 },
            };

            foreach (var ch in letter)
            {
                result += SymbolToVar[ch];
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
