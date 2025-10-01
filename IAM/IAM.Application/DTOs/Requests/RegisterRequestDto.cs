using IAM.Domain.ValueObjects;

namespace IAM.Application.DTOs.Requests
{
    public class RegisterRequestDto
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Sername { get; set; }
        
        public string? Fathername { get; set; }

        public ContactsDTO Contacts { get; set; } = null!;

        public string? Role { get; set; }
    }
}
