using Calculator;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace CHECalcApp.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private readonly CalculatorInput calculatorInput;

    public MainViewModel(CalculatorInput calculatorInput)
    {
        this.calculatorInput = calculatorInput;
    }

    [ObservableProperty]
    string text = "0";
    
    [ObservableProperty]
    string keyText = "0";

    [ObservableProperty]
    string equationText = string.Empty;

    string prevKey = string.Empty;
    decimal? prevValue = null;
    string prevOperator= string.Empty;
    string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    string[] operators = { "+", "-", "/", "*", "="};

    [RelayCommand]
    void InputKey(string key)
    {
        if (numbers.Contains(key))
        {
            var kText = KeyText;
            var eqText = EquationText;
            if (calculatorInput.Numbers(ref kText, ref prevKey, ref prevValue, ref prevOperator, ref eqText, key, numbers, operators))
            {
                Text = KeyText = kText;
                EquationText = eqText;
            }
        }
        else if (key == ".")
        {
            var kText = KeyText;
            var eqText = EquationText;
            if (calculatorInput.Point(ref kText, ref prevKey, ref prevValue, ref prevOperator, ref eqText, key, numbers, operators))
            {
                Text = KeyText = kText;
                EquationText = eqText;
            }
        }
        else if(key == "+/-")
        {
            var kText = KeyText;
            var eqText = EquationText;
            if (calculatorInput.Negate(ref kText, ref prevKey, ref prevValue, ref prevOperator, ref eqText, key, numbers, operators))
            {
                Text = KeyText = kText;
                EquationText = eqText;
            }
        }
        else if (operators.Contains(key))
        {
            //prevOperator = key;
            var kText = KeyText;
            var eqText = EquationText;
            if (calculatorInput.Operators(ref kText, ref prevKey, ref prevValue, ref prevOperator, ref eqText, key, numbers, operators))
            {
                Text = KeyText = kText;
                EquationText = eqText;
            }
        }
        else if (key == "C")
        {
            var kText = KeyText;
            var eqText = EquationText;
            if (calculatorInput.Cancel(ref kText, ref prevKey, ref prevValue, ref prevOperator, ref eqText, key, numbers, operators))
            {
                Text = KeyText = kText;
                EquationText = eqText;
            }
            
        }




        prevKey = key;
    }
}
    
