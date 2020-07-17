using Microsoft.AspNetCore.Mvc;
using UnitTestApp.Models;

namespace UnitTestApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository _repository;
        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        // Страница со списком всех пользователей
        public IActionResult Index() => View(_repository.GetAll());

        // Страница создания пользователя
        public IActionResult Create() => View();

        // Создание пользователя
        [HttpPost]
        public IActionResult Create(User model)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Получение информации о пользователе по Id
        public IActionResult GetUser(string id)
        {
            int intId;
            if (int.TryParse(id, out intId))
            {
                return View(_repository.Get(intId));
            }
            return View(null);
        }

        //Страница со списком пользователей младше 18 лет
        public IActionResult Less18() => View("Index", _repository.Where(p => p.Age < 18));
    }
}