using RomanNumeralConverter.ViewModels;

namespace RomanNumeralConverter
{
    public class MainWindowViewModel
    {
        public NumeralsViewModel NumeralsViewModel { get; set; }

        public MainWindowViewModel()
        {
            NumeralsViewModel = new NumeralsViewModel();
        }
    }
}
