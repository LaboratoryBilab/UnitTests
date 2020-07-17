using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestApp.Models
{
    public class Repository : IRepository
    {
        /// <summary>
        /// Имитация базы данных
        /// Имитируем, что бы не добавлять зависимости для запуска проекта
        /// В реально проекте так делать не нужно !
        /// </summary>
        private List<User> users = new List<User>()
        {
            new User(){ FirstName="Иван", Age=22, City="Волгоград" },
        };

        /// <summary>
        /// Создание записи User
        /// </summary>
        /// <param name="user">Запись User</param>
        public void Create(User user)
        {
            var id = users.Select(p => p.Id).Max() + 1;
            user.Id = id;
            users.Add(user);
        }

        /// <summary>
        ///  Получение пользователя по Id
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns>Пользователь</returns>
        public User Get(int id)
        {
            return users.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Получения списка всех пользователей
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        public IEnumerable<User> GetAll()
        {
            return users;
        }

        /// <summary>
        /// Отбор пользователей из БД с использование пользовательской функции
        /// </summary>
        /// <param name="func">Функция отбора записей</param>
        /// <returns>Записи удовлетворившие критериям поиска</returns>
        public IEnumerable<User> Where(Func<User, bool> func)
        {
            return users.Where(func);
        }
    }
}
