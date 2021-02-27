using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(160)]
        public string Address { get; set; }

        public ICollection<Ubication> Ubications {get; set;}
        public ICollection<AppUser> AppUsers {get; set;}
    }
}