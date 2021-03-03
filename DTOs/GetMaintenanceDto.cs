using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class GetMaintenanceDto
    {
        [Required]
        [MaxLength (25)]
        public string DeviceName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Priority { get; set; }

        public string Problem { get; set; }

        public string MaintenanceType { get; set; }

        public string Description { get; set; }   
    }
}