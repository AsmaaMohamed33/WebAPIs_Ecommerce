using System.ComponentModel.DataAnnotations;

namespace WebApplication8.DTO
{
    public class LoginDto
    {
        [Required]
        public string UserNAme { get; set; }

        [Required]
        public string PAssword { get; set; }
    }
}
