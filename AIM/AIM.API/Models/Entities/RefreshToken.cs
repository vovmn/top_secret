namespace AIM.API.Models.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expire { get; set; }
        
    }
}
