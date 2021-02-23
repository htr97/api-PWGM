using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class AppUser
    {
         public int Id { get; set; }

        [Required]
        public string UserName{ get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(75)]
        public string Country { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}