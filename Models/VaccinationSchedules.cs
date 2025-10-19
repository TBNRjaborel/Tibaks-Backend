using System.ComponentModel.DataAnnotations.Schema;

namespace Tibaks_Backend.Models
{
    public class VaccinationSchedules
    {
        public int Id { get; set; }
        public string ChildId { get; set; } = null!;
        public int VaccineId { get; set; }
        public DateOnly? NextSchedule { get; set; }

        [ForeignKey(nameof(ChildId))]
        public Child? Child { get; set; }

        [ForeignKey(nameof(VaccineId))]
        public Vaccine? Vaccine { get; set; }


    }
}
