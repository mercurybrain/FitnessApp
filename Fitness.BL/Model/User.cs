using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.BL.Model
{
    [Serializable]
    /// <summary>
    /// Пользователь 
    /// </summary>
    public class User
    {
        #region Параметры пользователя
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Пол пользователя
        /// </summary>
        public Gender Gender { get; }
        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime BirthDate { get; }
        /// <summary>
        /// Вес пользователя
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// Рост пользователя
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Вычисляемое поле для годиков
        /// </summary>
        public int Age { get {
                int age = DateTime.Today.Year - BirthDate.Year;
                if (BirthDate > DateTime.Today.AddYears(-age)) age--;
                return age;
            } } 
        #endregion
        /// <summary>
        /// Создать нового пользователя
        /// </summary>
        /// <param name="Name"> Имя </param>
        /// <param name="Gender"> Пол </param>
        /// <param name="BirthDate"> Дата рождения </param>
        /// <param name="Weight"> Вес </param>
        /// <param name="Height"> Рост </param>
        public User(string Name, Gender Gender, DateTime BirthDate, double Weight, double Height) {
            #region Проверка аргументов
            if (string.IsNullOrWhiteSpace(Name)) {
                throw new ArgumentNullException("Имя не может быть пустым или null: ", nameof(Name));
            }
            if (Gender == null) {
                throw new ArgumentNullException("Пол не может быть null: ", nameof(Gender));
            }
            if (BirthDate < DateTime.Parse("01.01.1900") || BirthDate > DateTime.Now) {
                throw new ArgumentException("Невозможная дата рождения: ", nameof(BirthDate));
            }
            if (Weight <= 0 || Weight >= 400)
            {
                throw new ArgumentException("Невозможный вес: ", nameof(Weight));
            }
            if (Height <= 0) {
                throw new ArgumentException("Невозможный рост: ", nameof(Height));
            }
            #endregion
            this.Name = Name;
            this.Gender = Gender;
            this.BirthDate = BirthDate;
            this.Weight = Weight;
            this.Height = Height;
        }

        public User(string Name) {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Имя не может быть пустым или null: ", nameof(Name));
            }
            this.Name = Name;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
