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
        public void ProcessNumbersTest()
        {
            var calculatorInput = new CalculatorInput();

            string[] inputKeys = {"1", "2", "3", "4", "5" };
            var actual = "0";
            var prevKey = string.Empty;
            foreach (var inputKey in inputKeys)
            {
                actual = calculatorInput.ProcessNumbers(actual, inputKey, prevKey);
                prevKey = inputKey;
            }

            Assert.AreEqual("11112345", actual);
        }

        [TestMethod]
        public void ProcessNumbersWith10Test()
        {
            var calculatorInput = new CalculatorInput();

            string[] inputKeys = { "1", "2", "3", "4", "5" };
            var actual = "10";
            var prevKey = "0";
            foreach (var inputKey in inputKeys)
            {
                actual = calculatorInput.ProcessNumbers(actual, inputKey,  prevKey);
                prevKey = inputKey;
            }

            Assert.AreEqual("1012345", actual);
        }

        [TestMethod]
        public void ProcessDecimalPointTest()
        {
            var calculatorInput = new CalculatorInput();

            string[] inputKeys = { "." };
            var actual = "0";
            var prevKey = string.Empty;
            foreach (var inputKey in inputKeys)
            {
                actual = calculatorInput.ProcessDecimalPoint(actual, inputKey, prevKey);
                prevKey = inputKey;
            }

            Assert.AreEqual("0.", actual);
        }

        [TestMethod]
        public void ProcessDecimalPointWith10Test()
        {
            var calculatorInput = new CalculatorInput();

            string[] inputKeys = { "." };
            var actual = "10";
            var prevKey = "0";
            foreach (var inputKey in inputKeys)
            {
                actual = calculatorInput.ProcessDecimalPoint(actual, inputKey, prevKey);
                prevKey = inputKey;
            }

            Assert.AreEqual("10.", actual);
        }

        [DataTestMethod]
        [DataRow("10+", "+", "0.")]
        [DataRow("10+", "-", "0.")]
        [DataRow("10+", "*", "0.")]
        [DataRow("10+", "/", "0.")]
        public void ProcessDecimalPointWithOperatorTest(string keyText, string prevKey, string expected)
        {
            var calculatorInput = new CalculatorInput();

            string[] inputKeys = { "." };
            //foreach (var inputKey in inputKeys)
            //{
             var   actual = calculatorInput.ProcessDecimalPoint(keyText, ".", prevKey);
                //prevKey = inputKey;
            //}

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ProcessOperatorPlusTest()
        {
            var calculatorInput = new CalculatorInput();

            string inputKey = "+";
            var keyText = "0";
            decimal? prevValue = null;
            string prevOperator = string.Empty;
            var actual = calculatorInput.ProcessOperator(ref prevValue, keyText, inputKey, prevOperator);

            Assert.AreEqual("0", actual);
        }


        [DataTestMethod]
        [DataRow("+", "2", "+", "12")]
        [DataRow("-", "2", "+", "8")]
        [DataRow("*", "2", "-", "20")]
        [DataRow("/", "2", "*", "5")]
        public void ProcessOperatorTest( string prevOperator, string keyText, string inputKey, string expected)
        {
            var calculatorInput = new CalculatorInput();


            decimal? prevValue = 10;
            var actual = calculatorInput.ProcessOperator(ref prevValue, keyText, inputKey, prevOperator);

            Assert.AreEqual(expected, actual);
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
        public void CancelNumberAfterOperatorTest(string keyText, string prevKey, string prevOperator, string equationText, string expected)
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


    }
}