using Tibaks_Backend.Data;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Tibaks_Backend.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly ApplicationDbContext _context;

        public HolidayService(ApplicationDbContext context)
        {
            _context = context;
        }

        private static HolidayDto MapToDto(Holiday holiday)
        {
            return new HolidayDto
            {
                Id = holiday.Id,
                Date = holiday.Date,
                
            };
        }
        public async Task<HolidayDto> CreateHolidayAsync(HolidayInputDto dto)
        {
            var holiday = new Holiday
            {
                Date = dto.Date,
                
            };
            _context.Holiday.Add(holiday);
            await _context.SaveChangesAsync();
            return MapToDto(holiday);
        }
        public async Task<IEnumerable<HolidayDto>> GetAllHolidaysAsync()
        {
            var holidays = await _context.Holiday.ToListAsync();
            return holidays.Select(MapToDto);
        }

        public async Task<HolidayDto> GetHolidayById(int id)
        {
            var holiday = await _context.Holiday
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);
            if (holiday == null)
                return null;
            return MapToDto(holiday);
        }
    }
}
