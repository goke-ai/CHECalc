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
        public string ProcessDecimalPoint(string keyText, string key, string prevKey)
        {
            string[] operators = { "+", "-", "/", "*" };

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
            //Text = KeyText;

            return keyText;
        }

        public string ProcessNumbers(string keyText, string key,  string prevKey)
        {
            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            //if (numbers.Contains(key))
            //{
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

                //Text = keyText;
            //}

            return keyText;
        }

        public string ProcessOperator(ref decimal? prevValue, string keyText, string key,  string prevOperator)
        {
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
            //Text = prevValue.Value.ToString("0.###############################");
            return FormatOutput(prevValue.Value);
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
                }

                prevKey = key;

                return true;
            }

            return false;
        }
    }
}
