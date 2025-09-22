namespace AIM.API.Models
{
    public class LoginResponseDto
    {
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; } // seconds
        public string? RefreshToken { get; set; }
    }
}
