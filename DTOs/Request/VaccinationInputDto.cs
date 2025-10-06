namespace Tibaks_Backend.DTOs.Request
{
    public class VaccinationInputDto
    {
        public string ChildId { get; set; } = null!;
        public int VaccineId { get; set; }
        public string BatchLotNumber { get; set; } = null!;
        public int DoseNumber { get; set; }
        public string Dosage { get; set; } = null!;
        public DateTime? DateAdministered { get; set; }
        public string? HealthcareWorkerId { get; set; }
    }
}
