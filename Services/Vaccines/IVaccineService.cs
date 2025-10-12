using Tibaks_Backend.DTOs.Response;

namespace Tibaks_Backend.Services
{
    public interface IVaccineService
    {
        Task<IEnumerable<VaccineDto>> GetAllAsync();
        Task<VaccineDto?> GetByIdAsync(int id);
    }
}
