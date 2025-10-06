using Tibaks_Backend.DTOs.Response;

namespace Tibaks_Backend.Services
{
    public interface IHealthcareWorkerService
    {
        Task<IEnumerable<HealthcareWorkerDto>> GetAllAsync();
        Task<HealthcareWorkerDto?> GetByIdAsync(string id);
    }
}
