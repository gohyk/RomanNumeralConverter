# Roman Numeral Converter
This application will allow user to convert from Arabic numeral to Roman Numeral and vice versa.
Type valid numerals in any text boxes to start the conversion.

##Assumptions/Limitations
1. User input
   1. Arabic Numeral should only take in 1 to 4999 and Roman numeral should only take in I to MMMMCMXCIX
      - There is no zero for roman numerals
      - 5000 is represented by another roman symbol which is not on the keyboard
   1. Roman numerals can take in lowercase/uppercase. But only uppercase will be displayed
   1. Roman numerals will not be able take in input if it does not match the Roman format, for example,
   "M" cannot appear 5 times consecutively, in this case, user input will not be reflected.
    
1. Software design considerations
    - This WPF application is created on .NET Core 3.1 framework
    - Although `NumeralsModel` is small and may be included in the `NumeralsViewModel`. This is designed 
      to future-proof the application, for example, there may be more ViewModels using `NumeralModel`.