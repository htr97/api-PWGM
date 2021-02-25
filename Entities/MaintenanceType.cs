using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class MaintenanceType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(160)]
        public string Description { get; set; }
    }
}