using System;
using System.Collections.Generic;
using System.Text;
using UnitTestApp.Controllers;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class CalculatorControllerTest
    {
        /// <summary>
        /// Тестируем Action - [Add]
        /// Сценарий - сумма чисел a и b
        /// Результат - сумма больше нуля
        /// </summary>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(0, 1)]
        [InlineData(-1, 2)]
        public void Add_APlusB_AboveZero(int a, int b)
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Add(a, b);
            // Assert
            Assert.True(result > 0);
        }

        /// <summary>
        /// Тестируем Action - [Add]
        /// Сценарий - сумма чисел a и b
        /// Результат - сумма менньше нуля
        /// </summary>
        [Theory]
        [InlineData(1, -2)]
        [InlineData(0, -1)]
        [InlineData(-1, -2)]
        public void Add_APlusB_LessThanZero(int a, int b)
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Add(a, b);
            // Assert
            Assert.True(result < 0);
        }


        /// <summary>
        /// Тестируем Action - [Div]
        /// Сценарий - деление числа на 0
        /// Результат - исключение деления на 0
        /// </summary>
        [Fact]
        public void Div_ADiv0_DivideByZeroException()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            Func<object> func = () => controller.Div(123, 0);
            // Assert
            Assert.Throws<DivideByZeroException>(func);
        }

        /// <summary>
        /// Тестируем Action - [Mul]
        /// Сценарий - умножение чисел с разными знаками
        /// Результат - число меньше нуля
        /// </summary>
        [Theory]
        [InlineData(-1, 1)]
        [InlineData(10, -10)]
        public void Mul_AMulB_AboveZero(int a, int b)
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Mul(a, b);
            // Assert
            Assert.True(result < 0);
        }

        /// <summary>
        /// Тестируем Action - [Mul]
        /// Сценарий - умножение 123 на 321
        /// Результат - 39483
        /// </summary>
        [Fact]
        public void Mul_123Mul321_39483Returned()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Mul(123, 321);
            // Assert
            Assert.Equal(39483, result);
        }


        /// <summary>
        /// Тестируем Action - [Mul]
        /// Сценарий - умножение 123 на 0
        /// Результат - 0
        /// </summary>
        [Fact]
        public void Mul_123Mul0_0Returned()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Mul(123, 0);
            // Assert
            Assert.Equal(0, result);
        }



        /// <summary>
        /// Тестируем Action - [Div]
        /// Сценарий - деление 39483 на 321
        /// Результат - 123
        /// </summary>
        [Fact]
        public void Mul_39483Mul321_123Returned()
        {
            // Arrange
            CalculatorController controller = new CalculatorController();
            // Act
            int result = controller.Div(39483, 321);
            // Assert
            Assert.Equal(123, result);
        }
    }
}
