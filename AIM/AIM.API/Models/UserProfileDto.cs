namespace AIM.API.Models
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }

        // Аутентификационные данные
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Персональные данные
        public string Name { get; set; }
        public string Sername { get; set; }
        public string Fathername { get; set; }

        // Дополнительная информация
        public string Messangers { get; set; }

        // Роли
        public Role Priveleges { get; set; }

        // Вычисляемое свойство для полного имени
        public string FullName => $"{Sername} {Name} {Fathername}";

    }
}
