using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class MaintenanceDto
    {
        [Required]
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(160)]
        public string Description { get; set; }
        public int MaintenanceTypeId { get; set; }
        public int PriorityId { get; set;}
        public int UserId { get; set; }
        public int ProblemID { get; set; }
        public int EquipmentId { get; set; }
    }
}