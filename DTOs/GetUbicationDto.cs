using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UbicationDto
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public int CompanyId { get; set; }

        public string Company { get; set; }
    }
}