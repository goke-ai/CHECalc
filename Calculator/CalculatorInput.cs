using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    public class CalculatorInput
    {

        public bool Numbers(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            if (numbers.Contains(key))
            {
                if (operators.Contains(prevKey))
                {
                    keyText = "0";
                }

                if (string.IsNullOrEmpty(prevKey) || keyText == "0")
                {
                    keyText = key;
                }
                else
                {
                    keyText += key;
                }

                if (prevOperator == "=")
                {
                    equationText = string.Empty;
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
                    equationText += keyText + " " + key + " ";

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

        public bool BracketOpen(ref string keyText, ref string prevKey, ref decimal? prevValue, ref string prevOperator, ref string equationText, string key, string[] numbers, string[] operators)
        {
            throw new NotImplementedException();
        }
    }
}
