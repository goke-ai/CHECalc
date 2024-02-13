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
            KeyText = calculatorInput.ProcessNumbers(KeyText, key, prevKey);
            Text = KeyText;

            if (prevOperator == "=")
            {
                EquationText = string.Empty;
            }
        }
        else if (key == ".")
        {
            KeyText = calculatorInput.ProcessDecimalPoint(KeyText, key, prevKey);
            Text = KeyText;
        }
        else if(key == "+/-")
        {
            //if(numbers.Contains(prevKey) )
            {
                var y = decimal.Parse(KeyText) * -1;
                KeyText = CalculatorInput.FormatOutput(y);
            }
            Text = KeyText;
        }
        else if (operators.Contains(key))
        {
            if (operators.Contains(prevKey))
            {
                if (prevOperator == "=")
                {
                    EquationText = Text + " " + key + " ";
                }
                else
                {
                    string searchText = (KeyText + " " + prevKey + " ");
                    int i = EquationText.LastIndexOf(searchText);

                    EquationText = EquationText.Remove(i);
                    EquationText += KeyText + " " + key + " ";
                }
            }
            else
            {
                EquationText += KeyText + " " + key + " ";

                
                Text = calculatorInput.ProcessOperator(ref prevValue, KeyText, key, prevOperator);

                
            }

            prevOperator = key;
        }
        else if (key == "C")
        {
            var k = KeyText;
            var eq = EquationText;
            if(calculatorInput.Cancel(ref k, ref prevKey, ref prevValue, ref prevOperator,  ref eq, key, numbers, operators))
            {
                Text = KeyText = k;
                EquationText = eq;
            }
            
        }




        prevKey = key;
    }
}
    
