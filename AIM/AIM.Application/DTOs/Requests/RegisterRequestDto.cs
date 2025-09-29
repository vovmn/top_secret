using AIM.Domain.ValueObjects;

namespace AIM.Application.DTOs.Requests
{
    public class RegisterRequestDto
    {
        public LoginInfo LoginInfo { get; set; } = null!;
        
        public string? Password { get; set; }

        public FIO FIO { get; set; } = null!;

        public Messengers Messangers { get; set; } = null!;

        public string? Role { get; set; }
    }
}
