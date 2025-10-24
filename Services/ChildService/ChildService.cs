using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.Models;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.Models.Enums;


namespace Tibaks_Backend.Services
{
    public class ChildService : IChildService
    {
        private readonly ApplicationDbContext _context;
        private readonly IVaccinationService _vaccinationService;
        public ChildService(ApplicationDbContext context, IVaccinationService vaccinationService)
        {
            _context = context;
            _vaccinationService = vaccinationService;
        }

        public async Task<ChildDto> CreateChildAsync(ChildInputDto dto)
        {
            var child = new Child
            {
                Image = dto.Image,
                UpdatedBy = dto.UpdatedBy,
                UpdatedAt = DateOnly.FromDateTime(DateTime.Now),
                ChildInfo = new ChildInfo
                {
                    FirstName = dto.ChildInfo.FirstName,
                    MiddleName = dto.ChildInfo.MiddleName,
                    LastName = dto.ChildInfo.LastName,
                    Suffix = dto.ChildInfo.Suffix,
                    Sex = dto.ChildInfo.Sex,
                    DateOfBirth = dto.ChildInfo.DateOfBirth,
                    BirthOrder = dto.ChildInfo.BirthOrder,
                    PlaceOfDelivery = dto.ChildInfo.PlaceOfDelivery,
                    BirthWeight = dto.ChildInfo.BirthWeight,
                    FeedingType = dto.ChildInfo.FeedingType,
                    DateReferredForNewbornScreening = dto.ChildInfo.DateReferredForNewbornScreening,
                    DateAssessed = dto.ChildInfo.DateAssessed
                },
                Address = new AddressInfo
                {
                    HomeAddress = dto.Address.HomeAddress,
                    NearestLandmark = dto.Address.NearestLandmark
                },
                Mother = new MotherInfo
                {
                    Name = dto.Mother.Name,
                    DateOfBirth = dto.Mother.DateOfBirth,
                    Occupation = dto.Mother.Occupation,
                    ContactNumber = dto.Mother.ContactNumber,
                    TetanusToxoidStatus = dto.Mother.TetanusToxoidStatus,
                    TetanusToxoidDate = dto.Mother.TetanusToxoidDate,
                    PhicNumber = dto.Mother.PhicNumber
                },
                Father = new FatherInfo
                {
                    Name = dto.Father.Name,
                    DateOfBirth = dto.Father.DateOfBirth,
                    Occupation = dto.Father.Occupation,
                    ContactNumber = dto.Father.ContactNumber
                }
            };

            _context.Children.Add(child);
            await _context.SaveChangesAsync();

            await _vaccinationService.InitializeSchedules(child.Id);
            await _vaccinationService.InitializeShots(child.Id);
            return MapToChildDto(child);
        }

        public async Task<ChildDto?> GetChildByIdAsync(string id)
        {
            var child = await _context.Children
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            return child == null ? null : MapToChildDto(child);
        }

        public async Task<IEnumerable<ChildDto>> GetAllChildrenAsync()
        {
            var children = await _context.Children
                .AsNoTracking()
                .OrderByDescending(c => c.ChildInfo.DateOfBirth)
                .ToListAsync();

            return children.Select(MapToChildDto);
        }

        public async Task<ChildDto?> UpdateChildAsync(string id, ChildInputDto dto)
        {
            var child = await _context.Children
                .Include(c => c.ChildInfo)
                .Include(c => c.Address)
                .Include(c => c.Mother)
                .Include(c => c.Father)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (child == null)
                return null;

            // Update scalar fields
            child.Image = dto.Image;

            // Update child info
            child.ChildInfo.FirstName = dto.ChildInfo.FirstName;
            child.ChildInfo.MiddleName = dto.ChildInfo.MiddleName;
            child.ChildInfo.LastName = dto.ChildInfo.LastName;
            child.ChildInfo.Suffix = dto.ChildInfo.Suffix;
            child.ChildInfo.Sex = dto.ChildInfo.Sex;
            child.ChildInfo.DateOfBirth = dto.ChildInfo.DateOfBirth;
            child.ChildInfo.BirthOrder = dto.ChildInfo.BirthOrder;
            child.ChildInfo.PlaceOfDelivery = dto.ChildInfo.PlaceOfDelivery;
            child.ChildInfo.BirthWeight = dto.ChildInfo.BirthWeight;
            child.ChildInfo.FeedingType = dto.ChildInfo.FeedingType;
            child.ChildInfo.DateReferredForNewbornScreening = dto.ChildInfo.DateReferredForNewbornScreening;
            child.ChildInfo.DateAssessed = dto.ChildInfo.DateAssessed;
            child.UpdatedBy = dto.UpdatedBy;
            child.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);

            // Update address
            child.Address.HomeAddress = dto.Address.HomeAddress;
            child.Address.NearestLandmark = dto.Address.NearestLandmark;

            // Update mother
            child.Mother.Name = dto.Mother.Name;
            child.Mother.DateOfBirth = dto.Mother.DateOfBirth;
            child.Mother.Occupation = dto.Mother.Occupation;
            child.Mother.ContactNumber = dto.Mother.ContactNumber;
            child.Mother.TetanusToxoidStatus = dto.Mother.TetanusToxoidStatus;
            child.Mother.TetanusToxoidDate = dto.Mother.TetanusToxoidDate;
            child.Mother.PhicNumber = dto.Mother.PhicNumber;

            // Update father
            child.Father.Name = dto.Father.Name;
            child.Father.DateOfBirth = dto.Father.DateOfBirth;
            child.Father.Occupation = dto.Father.Occupation;
            child.Father.ContactNumber = dto.Father.ContactNumber;

            await _context.SaveChangesAsync();
            return MapToChildDto(child);
        }

        public async Task<bool> DeleteChildAsync(string id)
        {
            var child = await _context.Children.FindAsync(id);
            if (child == null)
                return false;

            _context.Children.Remove(child);
            await _context.SaveChangesAsync();
            return true;
        }

        // Mapping method from Entity → DTO
        private static ChildDto MapToChildDto(Child child)
        {
            return new ChildDto
            {
                Id = child.Id,
                Image = child.Image,
                ChildInfo = new ChildInfoDto
                {
                    FirstName = child.ChildInfo.FirstName,
                    MiddleName = child.ChildInfo.MiddleName,
                    LastName = child.ChildInfo.LastName,
                    Suffix = child.ChildInfo.Suffix,
                    Sex = child.ChildInfo.Sex,
                    DateOfBirth = child.ChildInfo.DateOfBirth,
                    BirthOrder = child.ChildInfo.BirthOrder,
                    PlaceOfDelivery = child.ChildInfo.PlaceOfDelivery,
                    BirthWeight = child.ChildInfo.BirthWeight,
                    FeedingType = child.ChildInfo.FeedingType,
                    DateReferredForNewbornScreening = child.ChildInfo.DateReferredForNewbornScreening,
                    DateAssessed = child.ChildInfo.DateAssessed
                },
                Address = new AddressInfoDto
                {
                    HomeAddress = child.Address.HomeAddress,
                    NearestLandmark = child.Address.NearestLandmark
                },
                Mother = new MotherInfoDto
                {
                    Name = child.Mother.Name,
                    DateOfBirth = child.Mother.DateOfBirth,
                    Occupation = child.Mother.Occupation,
                    ContactNumber = child.Mother.ContactNumber,
                    TetanusToxoidStatus = child.Mother.TetanusToxoidStatus,
                    TetanusToxoidDate = child.Mother.TetanusToxoidDate,
                    PhicNumber = child.Mother.PhicNumber
                },
                Father = new FatherInfoDto
                {
                    Name = child.Father.Name,
                    DateOfBirth = child.Father.DateOfBirth,
                    Occupation = child.Father.Occupation,
                    ContactNumber = child.Father.ContactNumber
                }
            };
        }

        public async Task<List<ChildRecordDto>> GetChildrenWithVaccineStatus()
        {
            // First, get the basic vaccination data
            var vaccinationData = await _context.Vaccinations
                .Include(v => v.Child)
                    .ThenInclude(c => c.ChildInfo)
                .Include(v => v.Vaccine)
                .GroupBy(v => v.ChildId)
                .Select(g => new
                {
                    ChildId = g.Key,
                    Child = g.First().Child!,
                    TotalVaccinations = g.Count(),
                    AdministeredCount = g.Count(v => v.DateAdministered != null),
                    IncompleteVaccineIds = g.Where(v => v.DateAdministered == null)
                                           .Select(v => v.VaccineId)
                                           .ToList()
                })
                .ToListAsync();

            // Get all schedules separately
            var allSchedules = await _context.VaccinationSchedules
                .Include(s => s.Vaccine)
                .ToListAsync();

            // Process the data in memory
            var result = new List<ChildRecordDto>();

            foreach (var item in vaccinationData)
            {
                // Get schedules for this child
                var childSchedules = allSchedules.Where(s => s.ChildId == item.ChildId).ToList();

                // Determine status
                string status = "Unimmunized";
                if (item.AdministeredCount == 0)
                    status = "Unimmunized";
                else if (item.AdministeredCount == item.TotalVaccinations)
                    status = "Fully Vaccinated";
                else
                    status = "Partially Vaccinated";

                // Find earliest schedule for incomplete vaccines
                var earliestSchedule = childSchedules
                    .Where(s => item.IncompleteVaccineIds.Contains(s.VaccineId) && s.NextSchedule != null)
                    .OrderBy(s => s.NextSchedule)
                    .FirstOrDefault();

                DateOnly scheduleDate = earliestSchedule?.NextSchedule ?? default;

                // Get vaccine types for that schedule date
                var vaccineTypes = childSchedules
                    .Where(s => s.NextSchedule == scheduleDate && s.Vaccine != null)
                    .Select(s => s.Vaccine!.Name)
                    .Distinct()
                    .ToList();

                result.Add(new ChildRecordDto
                {
                    ChildId = item.Child.Id,
                    Name = item.Child.ChildInfo.FirstName + " " + item.Child.ChildInfo.LastName,
                    DateOfBirth = item.Child.ChildInfo.DateOfBirth,
                    Sex = item.Child.ChildInfo.Sex,
                    Status = status,
                    Schedule = scheduleDate,
                    VaccineType = vaccineTypes
                });
            }

            // Handle children with no vaccinations
            var childrenWithVaccinations = vaccinationData.Select(v => v.ChildId).ToHashSet();
            var allChildren = await _context.Children
                .Include(c => c.ChildInfo)
                .Where(c => !childrenWithVaccinations.Contains(c.Id))
                .ToListAsync();

            foreach (var child in allChildren)
            {
                var childSchedules = allSchedules.Where(s => s.ChildId == child.Id).ToList();
                var earliestSchedule = childSchedules
                    .Where(s => s.NextSchedule != null)
                    .OrderBy(s => s.NextSchedule)
                    .FirstOrDefault();

                DateOnly scheduleDate = earliestSchedule?.NextSchedule ?? default;
                var vaccineTypes = childSchedules
                    .Where(s => s.NextSchedule == scheduleDate && s.Vaccine != null)
                    .Select(s => s.Vaccine!.Name)
                    .Distinct()
                    .ToList();

                result.Add(new ChildRecordDto
                {
                    ChildId = child.Id,
                    Name = child.ChildInfo.FirstName + " " + child.ChildInfo.LastName,
                    DateOfBirth = child.ChildInfo.DateOfBirth,
                    Sex = child.ChildInfo.Sex,
                    Status = "Unimmunized",
                    Schedule = scheduleDate,
                    VaccineType = vaccineTypes
                });
            }

            return result;
        }

        //Dashboard Calls
        public async Task<Dictionary<string,int>> GetVaccinationStatusCounts()
        {
            var children = await GetChildrenWithVaccineStatus();

            var counts = children
                .GroupBy(c => c.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            return counts;
        }

        public async Task<int> GetChildrenCount()
        {
            return await _context.Children.CountAsync();
        }
    }
}