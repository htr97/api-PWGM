using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Equipment
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
        public string StorageCap { get; set; }

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

        public int Garantia { get; set; }
        public Ubication Ubication { get; set; }
        public int UbicationId { get; set; }

        public ICollection<Maintenance> Maintenances { get; set; }
    }
}