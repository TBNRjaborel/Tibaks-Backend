using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;

namespace Tibaks_Backend.Services
{
    public interface IVaccinationService
    {
        Task<VaccinationDto> CreateAsync(VaccinationInputDto dto);
        Task<VaccinationDto?> GetByIdAsync(string id);
        Task<IEnumerable<VaccinationDto>> GetByChildIdAsync(string childId);

        Task<IEnumerable<VaccinationDto>> GetAllAsync();
        Task<VaccinationDto?> UpdateAsync(string id, VaccinationInputDto dto);
        Task<bool> DeleteAsync(string id);

        Task<IEnumerable<VaccinationDto>> CreateManyAsync(List<VaccinationInputDto> requests);
        Task<IEnumerable<VaccinationDto>> UpdateManyAsync(List<VaccinationDto> requests);
        Task<VaccinationScheduleDto?> GetSchedulesByChildIdAsync(string childId);
        Task<IEnumerable<VaccinationScheduleDto>> UpdateSchedulesAsync(VaccinationScheduleInputDto inputDto);
        Task InitializeSchedules(string ChildId);
        Task InitializeShots(string ChildId);
    }
}
