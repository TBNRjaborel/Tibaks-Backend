using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Services
{
    public class HealthcareWorkerService : IHealthcareWorkerService
    {
        private readonly ApplicationDbContext _context;

        public HealthcareWorkerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HealthcareWorkerDto>> GetAllAsync()
        {
            return await _context.HealthcareWorkers
                .AsNoTracking()
                .Select(hw => new HealthcareWorkerDto
                {
                    Id = hw.Id,
                    Name = hw.Name,
                    UserId = hw.UserId,
                    Position = hw.Position
                })
                .ToListAsync();
        }

        public async Task<HealthcareWorkerDto?> GetByIdAsync(string id)
        {
            var hw = await _context.HealthcareWorkers
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hw == null)
                return null;

            return new HealthcareWorkerDto
            {
                Id = hw.Id,
                Name = hw.Name,
                UserId = hw.UserId,
                Position = hw.Position
            };
        }
    }
}
