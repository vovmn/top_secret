using AIM.API.Models.Entities;

namespace AIM.API.Models
{
    public class RegisterRequestDto
    {
        public LoginInfo LoginInfo { get; set; } = null!;
        
        public string? Password { get; set; }

        public FIO FIO { get; set; } = null!;

        public Messangers Messangers { get; set; } = null!;
    }
}
