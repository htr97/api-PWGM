using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Maintenance
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(160)]
        public string Description { get; set; }
        public MaintenanceType MaintenanceType { get; set; }
        public int MaintenanceTypeId { get; set; }
        public Priority Priority { get; set;}
        public int PriorityId { get; set;}
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public Problem Problem { get; set; }
        public int ProblemId { get; set; }
        public Equipment Equipment {get; set;}
        public int EquipmentId { get; set; }
    }
}