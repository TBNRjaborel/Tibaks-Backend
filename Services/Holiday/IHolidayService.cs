using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;

namespace Tibaks_Backend.Services
{
    public interface IHolidayService
    {
        Task<HolidayDto> CreateHolidayAsync(HolidayInputDto dto);
        Task<IEnumerable<HolidayDto>> GetAllHolidaysAsync();
        Task<HolidayDto> GetHolidayById(int id);
    }
}
