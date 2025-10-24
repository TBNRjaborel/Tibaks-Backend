using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Services
{
    public class TargetService : ITargetService
    {
        private readonly ApplicationDbContext _context;
        public TargetService(ApplicationDbContext context)
        {
            _context = context;
        }
        private static TargetDto MapToDto(Target target)
        {
            return new TargetDto
            {
                Id = target.Id,
                TargetNum = target.TargetNum
            };
        }
        public async Task<TargetDto> CreateTargetPatientsAsync(TargetInputDto dto)
        {
            var target = new Target
            {
                TargetNum = dto.TargetNum
            };
            _context.Target.Add(target);
            await _context.SaveChangesAsync();
            return MapToDto(target);
        }

        

        public async Task<TargetDto?> GetTargetByIdAsync(int id)
        {
            var target = await _context.Target
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
            if (target == null)
                return null;
            return MapToDto(target);
        }

        public async Task<TargetDto> UpdateTargetPatientsAsync(int id, TargetInputDto dto)
        {
            var target = await _context.Target.FindAsync(id);
            if (target == null)
            {
                throw new KeyNotFoundException("Target not found");
            }
            target.TargetNum = dto.TargetNum;
            await _context.SaveChangesAsync();
            return MapToDto(target);
        }
    }
}
