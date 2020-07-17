using Microsoft.AspNetCore.Mvc;
using UnitTestApp.Controllers;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class HomeControllerTest
    {
        /// <summary>
        /// Тестируем Action - [Index]
        /// Сценарий - получение значения ViewData["Message"]
        /// Результат - значение ViewData["Message"] должно равняться строке: "Hello world Unit-tests !"
        /// </summary>
        [Fact]
        public void Index_ViewData_Message()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.Equal("Hello world Unit-tests !", result?.ViewData["Message"]);
        }

        /// <summary>
        /// Тестируем Action - [Index]
        /// Сценарий - получение View 
        /// Результат - View не должна быть null
        /// </summary>
        [Fact]
        public void Index_ViewResult_NotNull()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.NotNull(result);
        }


        /// <summary>
        /// Тестируем Action - [Index]
        /// Сценарий - получение названия View
        /// Результат - название View явно не указано и рарвно null
        /// </summary>
        [Fact]
        public void Index_ViewName_EqualNull()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.Null(result?.ViewName);
        }
    }
}
