using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Services
{
    public interface IChildService
    {
        Task<ChildDto> CreateChildAsync(ChildInputDto dto);
        Task<ChildDto?> GetChildByIdAsync(string id);
        Task<IEnumerable<ChildDto>> GetAllChildrenAsync();
        Task<ChildDto?> UpdateChildAsync(string id, ChildInputDto dto);
        Task<bool> DeleteChildAsync(string id);
    }
}
