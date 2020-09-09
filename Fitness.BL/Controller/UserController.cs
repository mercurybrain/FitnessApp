using Fitness.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Fitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Пользователь приложения
        /// </summary>
        public List<User> Users { get; } //TODO: заменить на перечисление или RO-лист
        public User CurrentUser { get; }
        /// <summary>
        /// Создание нового контроллера пользователя
        /// </summary>
        /// <param name="User"></param>
        public UserController(string userName) {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("При создании бользователя имя не может быть пустым или null: ", nameof(userName));
            }
            Users = GetUsersData();
            CurrentUser = Users.SingleOrDefault(u => u.Name == userName);
            if (CurrentUser == null) {
                CurrentUser = new User(userName);
                Users.Add(CurrentUser);
                Save();
            }
        }
        /// <summary>
        /// Сохранить данные пользователя
        /// </summary>
        private void Save() {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate)) {
                formatter.Serialize(fs, Users);
            }
        }
        /// <summary>
        /// Получить сохраненный список пользователей
        /// </summary>
        /// <returns></returns>
        private List<User> GetUsersData() {
            var formatter = new BinaryFormatter();
            using (var fs = new FileStream("users.dat", FileMode.OpenOrCreate))
            {
                if (formatter.Deserialize(fs) is List<User> users) {
                    return users;
                }
            }
            return null;
        }
    }
}
