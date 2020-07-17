using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using UnitTestApp.Controllers;
using UnitTestApp.Models;
using Xunit;

namespace UnitTestApp.Tests.Controllers
{
    public class UsersControllerTest
    {
        private List<User> GetAllFakeUsers()
        {
            //Имена пользователей вымышлены, любые совпадения чистая случайность.
            return new List<User>()
            {
                new User(){ Id=1, FirstName="Алеша", LastName="Попович", Age=20, City="Русь" },
                new User(){ Id=2, FirstName="Илья", LastName="Муромец", Age=33, City="Русь" },
                new User(){ Id=3, FirstName="Добрыня", LastName="Никитич", Age=35, City="Русь" },
            };
        }

        /// <summary>
        /// Тестируем Action - [Index]
        /// Сценарий - получение результата выполнения контроллера (ViewResult) 
        /// Результат - View с данными типа "IEnumerable<User>"
        /// </summary>
        [Fact]
        public void Index_ViewResult_ViewResultAndListEntities()
        {
            // Arrange
            // Заменяем метод "реального" репозитория который работает с БД
            // это делается для того что бы ничего не сломать в рабочих данных
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.GetAll()).Returns(GetAllFakeUsers());
            var controller = new UsersController(mock.Object);

            // Act
            var result = controller.Index(); // Результат контроллера

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Действительно ли контроллер возвращает ViewResult
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model); // Проверяет, что модель в ViewResult типа "IEnumerable<User>" или производного типа.
            Assert.Equal(GetAllFakeUsers().Count, model.Count()); // Кол-во данных совпадает и контроллер ничего не добавил/удалил
        }


        /// <summary>
        /// Тестируем Action - [Create]
        /// Сценарий - создание пользователя с пустым именем 
        /// Результат - View с заполнеными полями и вывод сообщение об ошибках при заполнении формы
        /// </summary>
        [Fact]
        public void Create_CreateUserWithEmptyFirstName_ViewResult()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new UsersController(mock.Object);
            controller.ModelState.AddModelError("FirstName", "Required");
            User newUser = new User();

            // Act
            var result = controller.Create(newUser);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newUser, viewResult?.Model);
        }


        /// <summary>
        /// Тестируем Action - [Create]
        /// Сценарий - создание пользователя
        /// Результат - RedirectToActionResult
        /// </summary>
        [Fact]
        public void Create_CreateUser_RedirectToActionResult()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new UsersController(mock.Object);
            var newUser = new User()
            {
                FirstName = "Ben"
            };

            // Act
            var result = controller.Create(newUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mock.Verify(r => r.Create(newUser)); // Проверяем, был вызван ли метод Create у репозитория
        }

        /// <summary>
        /// Тестируем Action - [GetUser]
        /// Сценарий - Плохой запрос, с неверными данными
        /// Результат - ViewResult с пустой Model
        /// </summary>
        [Fact]
        public void GetUser_BadRequest_ViewResultWithEmptyModel()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var controller = new UsersController(mock.Object);

            // Act
            var result = controller.GetUser(null);

            // Arrange
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult?.Model);
        }



        /// <summary>
        /// Тестируем Action - [GetUser]
        /// Сценарий - Получение пользователя по Id
        /// Результат - ViewResult с данными пользователя
        /// </summary>
        [Fact]
        public void GetUser_GetUserById_ViewResultWithUser()
        {
            // Arrange
            int testUserId = 1;
            var mock = new Mock<IRepository>();
            mock.Setup(repo => repo.Get(testUserId))
                .Returns(GetAllFakeUsers().FirstOrDefault(p => p.Id == testUserId));
            var controller = new UsersController(mock.Object);

            // Act
            var result = controller.GetUser(testUserId.ToString());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<User>(viewResult.ViewData.Model);
            Assert.Equal("Алеша", model.FirstName);
            Assert.Equal("Попович", model.LastName);
            Assert.Equal("Русь", model.City);
            Assert.Equal(20, model.Age);
            Assert.Equal(testUserId, model.Id);
        }
    }
}
