using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class ProblemsDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(160)]
        public string Description { get; set; }
    }
}