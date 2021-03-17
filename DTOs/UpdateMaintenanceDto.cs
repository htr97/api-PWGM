using System;

namespace DTOs
{
    public class UpdateMaintenanceDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }  
        public int MaintenanceTypeId { get; set; }
        public int PriorityId { get; set; }
        public int ProblemId { get; set; }
        
    }
}