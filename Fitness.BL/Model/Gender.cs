using System;

namespace Fitness.BL.Model
{
    [Serializable]
    /// <summary>
    /// Класс пола
    /// Их же больше 50, поэтому и класс
    /// </summary>
    public class Gender
    {
        /// <summary>
        /// Название пола
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Создать новый пол
        /// </summary>
        /// <param name="name"></param>
        public Gender(string Name) {
            if (string.IsNullOrWhiteSpace(Name)) {
                throw new ArgumentNullException("Название пола не может быть пустым или null: ", nameof(Name));
            }
            this.Name = Name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
