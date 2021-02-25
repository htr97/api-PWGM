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

        [Required]
        public string Country { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Telephone { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength=4)]
        public string Password { get; set; }
    }
}