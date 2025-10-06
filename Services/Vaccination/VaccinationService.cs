using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Services
{
    public class VaccinationService : IVaccinationService
    {
        private readonly ApplicationDbContext _context;

        public VaccinationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VaccinationDto> CreateAsync(VaccinationInputDto dto)
        {
            var vaccination = new Vaccination
            {
                ChildId = dto.ChildId,
                VaccineId = dto.VaccineId,
                BatchLotNumber = dto.BatchLotNumber,
                DoseNumber = dto.DoseNumber,
                Dosage = dto.Dosage,
                DateAdministered = dto.DateAdministered,
                HealthcareWorkerId = dto.HealthcareWorkerId
            };

            _context.Vaccinations.Add(vaccination);
            await _context.SaveChangesAsync();

            return MapToDto(vaccination);
        }

        public async Task<VaccinationDto?> GetByIdAsync(string id)
        {
            var vaccination = await _context.Vaccinations.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
            return vaccination == null ? null : MapToDto(vaccination);
        }

        public async Task<IEnumerable<VaccinationDto>> GetByChildIdAsync(string childId)
        {
            return await _context.Vaccinations
                .Where(v => v.ChildId == childId)
                .Select(v => new VaccinationDto
                {
                    Id = v.Id,
                    ChildId = v.ChildId,
                    VaccineId = v.VaccineId,
                    BatchLotNumber = v.BatchLotNumber,
                    DoseNumber = v.DoseNumber,
                    Dosage = v.Dosage,
                    DateAdministered = v.DateAdministered,
                    HealthcareWorkerId = v.HealthcareWorkerId
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<VaccinationDto>> GetAllAsync()
        {
            var vaccinations = await _context.Vaccinations.AsNoTracking().ToListAsync();
            return vaccinations.Select(MapToDto);
        }

        public async Task<VaccinationDto?> UpdateAsync(string id, VaccinationInputDto dto)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
                return null;

            vaccination.ChildId = dto.ChildId;
            vaccination.VaccineId = dto.VaccineId;
            vaccination.BatchLotNumber = dto.BatchLotNumber;
            vaccination.DoseNumber = dto.DoseNumber;
            vaccination.Dosage = dto.Dosage;
            vaccination.DateAdministered = dto.DateAdministered;
            vaccination.HealthcareWorkerId = dto.HealthcareWorkerId;

            await _context.SaveChangesAsync();
            return MapToDto(vaccination);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
                return false;

            _context.Vaccinations.Remove(vaccination);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<VaccinationDto>> CreateManyAsync(List<VaccinationInputDto> requests)
        {
            var vaccinations = requests.Select(r => new Vaccination
            {
                Id = Guid.NewGuid().ToString(),
                ChildId = r.ChildId,
                VaccineId = r.VaccineId,
                BatchLotNumber = r.BatchLotNumber,
                DoseNumber = r.DoseNumber,
                Dosage = r.Dosage,
                DateAdministered = r.DateAdministered,
                HealthcareWorkerId = r.HealthcareWorkerId
            }).ToList();

            _context.Vaccinations.AddRange(vaccinations);
            await _context.SaveChangesAsync();

            return vaccinations.Select(MapToDto);
        }

        public async Task<IEnumerable<VaccinationDto>> UpdateManyAsync(List<VaccinationDto> requests)
        {
            var updatedList = new List<VaccinationDto>();

            foreach (var request in requests)
            {
                var vaccination = await _context.Vaccinations.FindAsync(request.Id);
                if (vaccination == null)
                {
                    // Skip silently or throw — here we'll skip
                    continue;
                }

                vaccination.ChildId = request.ChildId;
                vaccination.VaccineId = request.VaccineId;
                vaccination.BatchLotNumber = request.BatchLotNumber;
                vaccination.DoseNumber = request.DoseNumber;
                vaccination.Dosage = request.Dosage;
                vaccination.DateAdministered = request.DateAdministered;
                vaccination.HealthcareWorkerId = request.HealthcareWorkerId;

                _context.Vaccinations.Update(vaccination);
                updatedList.Add(MapToDto(vaccination));
            }

            await _context.SaveChangesAsync();
            return updatedList;
        }

        private static VaccinationDto MapToDto(Vaccination vaccination)
        {
            return new VaccinationDto
            {
                Id = vaccination.Id,
                ChildId = vaccination.ChildId,
                VaccineId = vaccination.VaccineId,
                BatchLotNumber = vaccination.BatchLotNumber,
                DoseNumber = vaccination.DoseNumber,
                Dosage = vaccination.Dosage,
                DateAdministered = vaccination.DateAdministered,
                HealthcareWorkerId = vaccination.HealthcareWorkerId
            };
        }
    }
}
