using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace RomanNumeralConverter.Model
{
    public class NumeralsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private int arabic;
        public int Arabic
        {
            get { return arabic; }
            set { arabic = value; OnPropertyChanged("Arabic"); }
        }

        private int roman;
        public int Roman
        {
            get { return roman; }
            set { roman = value; OnPropertyChanged("Roman"); }
        }
    }
}
