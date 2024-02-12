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
    public class CalculatorTests
    {
        [TestMethod]
        public void CalculatorNullTest()
        {
            var calculator = new Calculator();
            Assert.IsNotNull(calculator);
        }

        [TestMethod]
        public void AddTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Add(1, 1);
            var subtractActual = calculator.Subtract(actual, 1) == 1;

            // Assert
            Assert.IsNotNull(calculator);
            Assert.AreEqual(2, actual);
            Assert.IsTrue(subtractActual);
            StringAssert.Contains(actual.ToString(), "2");

            //// Same asserts as what is commented out above, but using Fluent Assertions
            //actual.Should().Be(2).And.NotBe(1);
        }

        [DataTestMethod]
        [DataRow(1, 1, 2)]
        [DataRow(2, 2, 4)]
        [DataRow(3, 3, 6)]
        [DataRow(0, 0, 1)] // The test run with this row fails
        public void AddDataTests(int x, int y, int expected)
        {
            var calculator = new Calculator();
            var actual = calculator.Add(x, y);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SubtractTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Subtract(1, 1);
            var addActual = calculator.Add(actual, 1) == 1;

            // Assert
            Assert.IsNotNull(calculator);
            Assert.AreEqual(0, actual);
            Assert.IsTrue(addActual);
            StringAssert.Contains(actual.ToString(), "0");
        }

        [TestMethod]
        public void MultiplyTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Multiply(2, 3);
            var divideActual = calculator.Divide(actual, 3) == 2;

            // Assert
            Assert.IsNotNull(calculator);
            Assert.AreEqual(6, actual);
            Assert.IsTrue(divideActual);
            StringAssert.Contains(actual.ToString(), "6");
        }

        [TestMethod]
        public void DivideTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Divide(10, 2);
            var multiplyActual = calculator.Multiply(actual!.Value, 2) == 10;

            // Assert
            Assert.IsNotNull(calculator);
            Assert.AreEqual(5, actual);
            Assert.IsTrue(multiplyActual);
            StringAssert.Contains(actual.ToString(), "5");
        }

        [TestMethod]
        public void DivideByZeroTest()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var actual = calculator.Divide(1, 0);

            // Assert
            Assert.IsNotNull(calculator);
            Assert.IsNull(actual);
            // Assert.IsTrue(multiplyActual);
            // StringAssert.Contains(actual.ToString(), "5");
        }
    }
}