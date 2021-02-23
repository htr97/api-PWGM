using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName{ get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string Password { get; set; }
    }
}