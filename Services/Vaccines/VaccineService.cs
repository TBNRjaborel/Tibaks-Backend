using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.DTOs.Response;


namespace Tibaks_Backend.Services
{
    public class VaccineService : IVaccineService
    {
        private readonly ApplicationDbContext _context;

        public VaccineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VaccineDto>> GetAllAsync()
        {
            return await _context.Vaccines
                .AsNoTracking()
                .Select(v => new VaccineDto
                {
                    Id = v.Id,
                    Name = v.Name,
                    RecommendedDosage = v.RecommendedDosage,
                    Description = v.Description,
                    Route = v.Route,
                    SiteOfAdministration = v.SiteOfAdministration
                })
                .ToListAsync();
        }

        public async Task<VaccineDto?> GetByIdAsync(int id)
        {
            var vaccine = await _context.Vaccines
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vaccine == null)
                return null;

            return new VaccineDto
            {
                Id = vaccine.Id,
                Name = vaccine.Name,
                RecommendedDosage = vaccine.RecommendedDosage,
                Description = vaccine.Description,
                Route = vaccine.Route,
                SiteOfAdministration = vaccine.SiteOfAdministration
            };
        }
    }
}