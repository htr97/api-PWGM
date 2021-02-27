using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Priority
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Description { get; set; }
    }
}