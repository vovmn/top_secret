using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public DateTime Expires { get; set; }
        
    }
}
