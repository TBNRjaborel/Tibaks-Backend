using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tibaks_Backend.Models
{
    public class HealthcareWorker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public string? UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Position { get; set; } = null!;
    }
}
