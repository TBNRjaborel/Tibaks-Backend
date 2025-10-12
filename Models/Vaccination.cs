using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tibaks_Backend.Models
{
    public class Vaccination
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string ChildId { get; set; } = null!;

        [Required]
        public int VaccineId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BatchLotNumber { get; set; } = null!;

        [Required]
        public int DoseNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Dosage { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateOnly? DateAdministered { get; set; }

        public string? HealthcareWorkerId { get; set; }

        // Navigation properties
        [ForeignKey(nameof(ChildId))]
        public Child? Child { get; set; }

        [ForeignKey(nameof(VaccineId))]
        public Vaccine? Vaccine { get; set; }

        [ForeignKey(nameof(HealthcareWorkerId))]
        public HealthcareWorker? HealthcareWorker { get; set; }
    }
}
