using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class EquipmentDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength (25)]
        public string DeviceName { get; set; }

        [Required]
        [MaxLength (25)]
        public string SystemType { get; set; }

        [Required]
        [MaxLength (25)]
        public string StorageType { get; set; }

        [Required]
        [MaxLength (25)]
        public string Processor { get; set; }

        [Required]
        [MaxLength (25)]
        public string Memory { get; set; }

        [Required]
        [MaxLength (25)]
        public string OsName { get; set; }

        [Required]
        [MaxLength (160)]
        public string Observation { get; set; }

        public string Ubication { get; set; }

        public int CompanyId { get; set; }
    }
}