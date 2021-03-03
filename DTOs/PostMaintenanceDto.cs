using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class PostMaintenanceDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }  
        public int MaintenanceTypeId { get; set; }
        public int PriorityId { get; set; }
        public int UserId { get; set; }
        public int ProblemId { get; set; }
        public int EquipmentId { get; set; }
        
    }
}