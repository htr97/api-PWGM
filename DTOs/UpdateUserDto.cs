using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string UserName{ get; set; }
        [Required]
        public DateTime DateofBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(75)]
        public string Country { get; set; }

        [StringLength(15)]
        public string Telephone { get; set; }
        public string Photo { get; set; }
    }
}