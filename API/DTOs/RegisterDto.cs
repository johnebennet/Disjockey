using System.ComponentModel.DataAnnotations;

namespace API.DTOs {
    public class RegisterDto {
        [Required]
        public string Username { get; set; }
        [Required]
        public long DiscordId { get; set; }
    }
}