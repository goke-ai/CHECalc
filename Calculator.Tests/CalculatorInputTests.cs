using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Tests
{
    [TestClass()]
    public class CalculatorInputTests
    {

        [TestMethod]
        public void Test()
        {
            // Arrange

            // Act

            // Assert
        }

        public void CalculatorInputNullTest()
        {
            var calculatorInput = new CalculatorInput();
            Assert.IsNotNull(calculatorInput);
        }

        [TestMethod]

        public void NumbersWithKeysTest()
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            string keyText = "0";
            string prevKey = "";
            string prevOperator = "";
            string equationText = "";
            string []keys = { "1","2","3","4","5"};

            bool isDone = false;
            // Act
            foreach (var key in keys)
            {
                isDone = calculatorInput.Numbers(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);
                if (!isDone)
                    break;
            }

            // Assert
            Assert.AreEqual("12345", keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual("5", prevKey);
            Assert.IsNull(prevValue);
            Assert.IsTrue(string.IsNullOrEmpty(prevOperator));
            Assert.IsTrue(string.IsNullOrEmpty(equationText));
        }

        [DataTestMethod]
        //[DataRow("0", "", "", "", "1", "0")]
        [DataRow("0", "", "", "", "1", "1")]
        [DataRow("123", "3", "", "", "1", "1231")]
        [DataRow("123.", ".", "", "", "1", "123.1")]

        public void NumbersTest(string keyText, string prevKey, string prevOperator, string equationText, string key, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };


            // Act
            bool isDone = calculatorInput.Numbers(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.IsNull(prevValue);
            Assert.IsTrue(string.IsNullOrEmpty(prevOperator));
            Assert.IsTrue(string.IsNullOrEmpty(equationText));
        }

        [DataTestMethod]
        [DataRow("123", "+", "+", "123 + ", "1", "1")]
        [DataRow("123", "-", "-", "123 - ", "1", "1")]
        [DataRow("123", "*", "*", "123 * ", "1", "1")]
        [DataRow("123", "/", "/", "123 / ", "1", "1")]
        [DataRow("123", "=", "=", "123 = ", "1", "1")]

        public void NumbersAfterOperatorTest(string keyText, string prevKey, string prevOperator, string equationText, string key, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            var prevOperatorExpected = prevOperator;
            var equationTextExpected = equationText;

            // Act
            bool isDone = calculatorInput.Numbers(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.IsNull(prevValue);
            Assert.AreEqual(prevOperatorExpected, prevOperator);
            if (prevOperator == "=")
            {
                Assert.IsTrue(string.IsNullOrEmpty(equationText));
            }
            else
            {
                Assert.AreEqual(equationTextExpected, equationText);
            }
        }


        [DataTestMethod]
        [DataRow("0", "", "", "", "0.")]
        [DataRow("1", "1", "", "", "1.")]

        public void PointTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            string key = ".";

            var prevOperatorExpected = prevOperator;
            var equationTextExpected = equationText;

            // Act
            bool isDone = calculatorInput.Point(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.IsNull(prevValue);
            Assert.AreEqual(prevOperatorExpected, prevOperator);
            Assert.AreEqual(equationTextExpected, equationText);
        }

        [DataTestMethod]
        [DataRow("0", "+", "+", "0 + ", "0.")]
        [DataRow("1", "+", "+", "1 + ", "0.")]
        [DataRow("1", "-", "-", "1 - ", "0.")]
        [DataRow("1", "*", "*", "1 * ", "0.")]
        [DataRow("1", "/", "/", "1 / ", "0.")]
        [DataRow("1", "=", "=", "1 = ", "0.")]

        public void PointAfterOperatorTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            string key = ".";

            var prevOperatorExpected = prevOperator;
            var equationTextExpected = equationText;

            // Act
            bool isDone = calculatorInput.Point(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.IsNull(prevValue);
            Assert.AreEqual(prevOperatorExpected, prevOperator);
            Assert.AreEqual(equationTextExpected, equationText);
        }


        [DataTestMethod]
        //[DataRow("0", "", "", "", "1", "0")]
        [DataRow("0", "", "", "", "+", "0")]
        [DataRow("1", "1", "", "", "+", "1")]
        [DataRow("0", "", "", "", "-", "0")]
        [DataRow("1", "1", "", "", "-", "1")]
        [DataRow("1", "1", "", "", "*", "1")]
        [DataRow("1", "1", "", "", "/", "1")]
        [DataRow("1", "1", "", "", "=", "1")]

        public void OperatorsTest(string keyText, string prevKey, string prevOperator, string equationText, string key, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            var prevValueExpected = decimal.Parse(keyText);
            var equationTextExpected = keyText + " " + key + " ";
            // Act
            bool isDone = calculatorInput.Operators(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.AreEqual(prevValueExpected, prevValue);
            Assert.AreEqual(key, prevOperator);
            Assert.AreEqual(equationTextExpected, equationText);
        }

        [DataTestMethod]
        //[DataRow("0", "", "", "", "1", "0")]
        [DataRow("1", "1", "-", "10 - ", "+", "9")]
        [DataRow("1", "1", "-", "10 - ", "-", "9")]
        [DataRow("1", "1", "-", "10 - ", "/", "9")]
        [DataRow("1", "1", "-", "10 - ", "*", "9")]
        [DataRow("1", "1", "-", "10 - ", "=", "9")]
        [DataRow("12", "2", "-", "10 - ", "+", "-2")]
        [DataRow("12", "2", "+", "10 + ", "+", "22")]

        public void OperatorsWithPrevValue10Test(string keyText, string prevKey, string prevOperator, string equationText, string key, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = 10;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            var prevValueExpected = decimal.Parse(expected);
            var equationTextExpected = equationText + keyText + " " + key + " ";
            // Act
            bool isDone = calculatorInput.Operators(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.AreEqual(prevValueExpected, prevValue);
            Assert.AreEqual(key, prevOperator);
            Assert.AreEqual(equationTextExpected, equationText);
        }



        [DataTestMethod]
        [DataRow("0", "", "", "", "0")]
        [DataRow("1", "", "", "", "0")]
        [DataRow("12", "2", "", "", "0")]
        public void CancelTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();
            var key = "C";
            decimal? prevValue = 10;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };


            // Act
            bool isDone = calculatorInput.Cancel(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.AreEqual("", equationText);
        }

        [DataTestMethod]
        [DataRow("2", "+", "", "1 + ", "0")]
        public void CancelWithNumberAfterOperatorTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();
            var key = "C";
            decimal? prevValue = 10;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };


            // Act
            bool isDone = calculatorInput.Cancel(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.AreEqual("1 + ", equationText);
        }

        [DataTestMethod]
        [DataRow("0", "C", "+", "1 + ", "0")]
        [DataRow("3", "=", "=", "1 + 2 = ", "0")]
        public void CancelCancelTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();
            var key = "C";
            decimal? prevValue = 10;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };


            // Act
            bool isDone = calculatorInput.Cancel(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            Assert.IsNull(prevValue);
            Assert.IsTrue(string.IsNullOrEmpty(prevOperator));
            Assert.IsTrue(string.IsNullOrEmpty(equationText));
        }


        [DataTestMethod]
        [DataRow("0", "", "", "", "0")]
        [DataRow("1", "1", "", "", "-1")]
        [DataRow("-1", "+/-", "", "", "1")]

        public void NegateTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = null;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };
            string[] unaries = { "+/-" };

            string key = "+/-";

            //var prevValueExpected = prevValue;
            //var prevOperatorExpected = prevOperator;
            //var equationTextExpected = equationText;

            // Act
            bool isDone = calculatorInput.Negate(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            //Assert.AreEqual(prevValueExpected, prevValue);
            //Assert.AreEqual(prevOperatorExpected, prevOperator);
            //Assert.AreEqual(equationTextExpected, equationText);
        }


        [DataTestMethod]
        [DataRow("10", "+", "+", "3 + 7 + ", "-10")]

        public void NegateWithPreValue10Test(string keyText, string prevKey, string prevOperator, string equationText, string expected)
        {
            // Arrange
            var calculatorInput = new CalculatorInput();

            decimal? prevValue = 10;

            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] operators = { "+", "-", "/", "*", "=" };

            string key = "+/-";

            //var prevValueExpected = prevValue;
            //var prevOperatorExpected = prevOperator;
            //var equationTextExpected = equationText;

            // Act
            bool isDone = calculatorInput.Negate(ref keyText, ref prevKey, ref prevValue, ref prevOperator, ref equationText, key, numbers, operators);

            // Assert
            Assert.AreEqual(expected, keyText);
            Assert.IsTrue(isDone);
            Assert.AreEqual(key, prevKey);
            //Assert.AreEqual(prevValueExpected, prevValue);
            //Assert.AreEqual(prevOperatorExpected, prevOperator);
            //Assert.AreEqual(equationTextExpected, equationText);
        }

        

    }
}