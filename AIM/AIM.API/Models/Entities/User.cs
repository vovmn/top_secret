using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор объекта.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Название объекта благоустройства (например, "Парк Горького, сектор А").
        /// </summary>
        [Required]
        public string UserName { get; private set; }

        public string EMail { get; private set; }

        public string PhoneNumber { get; private set; }


        /// <summary>
        /// Физический адрес объекта.
        /// </summary>
        [Required]
        public string Password { get; private set; }

        /// <summary>
        /// Текущий статус жизненного цикла объекта (Planned, Active, Completed).
        /// Согласно ТЗ, объект должен быть активирован перед началом работ.
        /// </summary>
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Плановая дата начала работ на объекте.
        /// </summary>
        [Required]
        public string Sername { get; private set; }

        /// <summary>
        /// Плановая дата окончания работ на объекте.
        /// </summary>
        public string Fathername { get; private set; }
        
        public string Messangers { get; private set; }

        /// <summary>
        /// Географический полигон, задающий границы рабочей зоны объекта.
        /// Используется для верификации присутствия пользователей на объекте (требование ТЗ п.3).
        /// </summary>
        [Required]
        public Role Priveleges { get; private set; }

        public User(Guid id, string userName, string eMail, string phoneNumber, string password, string name, string sername, string fathername, string messangers, Role priveleges)
        {
            Id = id;
            UserName = userName;
            EMail = eMail;
            PhoneNumber = phoneNumber;
            Password = password;
            Name = name;
            Sername = sername;
            Fathername = fathername;
            Messangers = messangers;
            Priveleges = priveleges;
        }

        /// <summary>
        /// Приватный конструктор для EF Core
        /// </summary>
        private User()
        {
            Name = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
            Sername = string.Empty;
            Priveleges = Role.NONE;
        }

        
    }
}
