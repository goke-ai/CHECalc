using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public class CalculatorStore
    {
        public CalculatorStore(string keyText, string prevKey, string equationText, string prevOperator, decimal? prevValue)
        {
            KeyText = keyText;
            EquationText = equationText;
            PrevKey = prevKey;
            PrevValue = prevValue;
            PrevOperator = prevOperator;
        }

        public string KeyText { get; }
        public string EquationText { get; }
        public string PrevKey { get; }
        public decimal? PrevValue { get; }
        public string PrevOperator { get; }
    }

    public class CalculatorInput
    {
        public List<EquationHistory> EquationHistories { get; private set; } = new();

        public bool Numbers(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (numbers.Contains(key))
            {
                if (prevOperator == "=")
                {
                    // store in history
                    EquationHistories.Add(new EquationHistory { EquationText = equationText, Value = keyText });
                    equationText = string.Empty;
                }

                if (operators.Contains(prevKey))
                {
                    keyText = "0";
                }

                if (string.IsNullOrEmpty(prevKey) || prevKey == "(" || keyText == "0")
                {
                    keyText = key;
                }
                else
                {
                    keyText += key;
                }               

                prevKey = key;

                return true;
            }
            return false;
        }

        public bool Point(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (key == ".")
            {
                if (operators.Contains(prevKey))
                {
                    keyText = "0";
                }

                if (!keyText.Contains(key))
                {
                    if (string.IsNullOrEmpty(prevKey) || keyText == "0")
                    {
                        keyText = "0.";
                    }
                    else
                    {
                        keyText += key;
                    }
                }

                prevKey = key;

                return true;
            }
            return false;
        }

        public bool Operators(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (operators.Contains(key))
            {
                if (operators.Contains(prevKey))
                {
                    if (prevOperator == "=")
                    {
                        equationText = keyText + " " + key + " ";
                    }
                    else
                    {
                        string searchText = (keyText + " " + prevKey + " ");
                        int i = equationText.LastIndexOf(searchText);

                        equationText = equationText.Remove(i);
                        equationText += keyText + " " + key + " ";
                    }
                }
                else
                {
                    if (prevKey == ")")
                    {
                        equationText += key + " ";
                    }
                    else
                    {
                        equationText += keyText + " " + key + " ";
                    }

                    if (prevValue is null)
                    {
                        prevValue = decimal.Parse(keyText);
                    }
                    else
                    {
                        switch (prevOperator)
                        {
                            case "+":
                                prevValue += decimal.Parse(keyText);
                                break;
                            case "-":
                                prevValue -= decimal.Parse(keyText);
                                break;
                            case "/":
                                prevValue /= decimal.Parse(keyText);
                                break;
                            case "*":
                                prevValue *= decimal.Parse(keyText);
                                break;
                            case "=":
                                prevValue = decimal.Parse(keyText);
                                break;
                            default:
                                break;
                        }
                    }
                    keyText = FormatOutput(prevValue.Value);
                }                

                prevOperator = prevKey = key;

                return true;
            }

            return false;
        }

        public static string FormatOutput(decimal value)
        {
            return value.ToString("0.###############################");
        }

        public bool Cancel(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (key == "C")
            {
                if (string.IsNullOrEmpty(prevKey))
                {
                    keyText = "0";
                }
                else
                {
                    if (numbers.Contains(prevKey) || operators.Contains(prevKey))
                    {
                        keyText = "0";
                    }

                    if (prevKey == key)
                    {
                        prevValue = null;
                        prevOperator = string.Empty;
                        equationText = string.Empty;
                    }

                    if(prevOperator == "=")
                    {
                        prevValue = null;
                        prevOperator = string.Empty;
                        equationText = string.Empty;
                    }
                }

                prevKey = key;

                return true;
            }

            return false;
        }

        public bool Negate(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (key == "+/-")
            {
                var y = decimal.Parse(keyText) * -1;
                keyText = CalculatorInput.FormatOutput(y);

                prevKey = key;

                return true;
            }

            return false;
        }

        public bool BracketOpen(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators, List<CalculatorStore> stores)
        {
            // 
            if (key == "(")
            {
                if (prevValue == null)
                {
                    if (keyText == "0")
                    {
                        equationText = "(";
                        stores.Add(new CalculatorStore(keyText, prevKey, equationText, "*", 1));
                    }
                    else
                    {
                        equationText = keyText + " x (";

                        prevValue = decimal.Parse(keyText);
                        stores.Add(new CalculatorStore(keyText, prevKey, equationText, "*", prevValue));
                    }
                }
                
                else
                {
                    if (prevKey == ")")
                    {
                        switch (prevOperator)
                        {
                            case "+":
                                prevValue += decimal.Parse(keyText);
                                break;
                            case "-":
                                prevValue -= decimal.Parse(keyText);
                                break;
                            case "/":
                                prevValue /= decimal.Parse(keyText);
                                break;
                            case "*":
                                prevValue *= decimal.Parse(keyText);
                                break;
                            case "=":
                                prevValue = decimal.Parse(keyText);
                                break;
                            default:
                                break;
                        }
                        keyText = FormatOutput(prevValue.Value);

                        equationText += "x (";
                    }
                    else
                    {
                        keyText = "0";
                        equationText += "(";

                    }
                    stores.Add(new CalculatorStore(keyText, prevKey, equationText, prevOperator, prevValue));

                }

                prevValue = null;
                prevOperator = string.Empty;               


                prevKey = key;

                return true;
            }

            return false;

        }

        public bool BracketClose(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators, List<CalculatorStore> stores)
        {
            // 
            if (key == ")" && stores.Count > 0)
            {
                if (prevValue == null)
                {
                    //if (keyText == "0")
                    //{
                    //    equationText += "0)";
                    //}
                    //else
                    {
                        equationText += keyText + ") ";
                    }
                }
                else
                {
                    equationText += keyText + ") ";

                    switch (prevOperator)
                    {
                        case "+":
                            prevValue += decimal.Parse(keyText);
                            break;
                        case "-":
                            prevValue -= decimal.Parse(keyText);
                            break;
                        case "/":
                            prevValue /= decimal.Parse(keyText);
                            break;
                        case "*":
                            prevValue *= decimal.Parse(keyText);
                            break;
                        case "=":
                            prevValue = decimal.Parse(keyText);
                            break;
                        default:
                            break;
                    }

                    keyText = FormatOutput(prevValue.Value);
                }

                var store = stores.Last();
                prevValue = store.PrevValue;
                prevOperator = store.PrevOperator;

                //remove last stored
                stores.RemoveAt(stores.Count - 1);

                prevKey = key;

                return true;
            }

            return false;

        }
    }
}
