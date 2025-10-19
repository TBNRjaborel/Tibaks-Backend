 namespace Tibaks_Backend.DTOs.Request
{
    public class VaccinationScheduleInputDto
    {
        
        public string ChildId { get; set; } = null!;
        public List<CreateVaccinationScheduleInputDto> VaccinationSchedules { get; set; } = null!;
    }

    public class CreateVaccinationScheduleInputDto
    {
        public string VaccineId { get; set; } = null!;
        public DateOnly? NextSchedule { get; set; }


    }
}
