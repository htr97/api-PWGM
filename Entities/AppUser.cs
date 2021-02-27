using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Extensions;

namespace Entities
{
    public class AppUser
    {
         public int Id { get; set; }

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

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public string Photo { get; set; }
        public Company Company {get; set;}
        public int CompanyId {get; set;}

        public int GetAge()
        {
            return DateofBirth.Calculateage();
        }
    }
}