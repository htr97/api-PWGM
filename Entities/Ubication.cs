using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Ubication
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Company Company {get; set;}
        public int CompanyId { get; set; }
    }
}