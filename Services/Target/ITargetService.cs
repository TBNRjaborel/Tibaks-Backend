using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;

namespace Tibaks_Backend.Services
{
    public interface ITargetService
    {
        Task<TargetDto> CreateTargetPatientsAsync(TargetInputDto dto);

        Task<TargetDto?> GetTargetByIdAsync(int id);
        Task<TargetDto> UpdateTargetPatientsAsync(int id, TargetInputDto dto);
    }
}
