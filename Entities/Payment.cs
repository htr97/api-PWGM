using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }

        public Company Company { get; set;}
        public int CompanyId { get; set;}

        [Required]
        [MaxLength(1)]
        public string Status { get; set; }
    }
}