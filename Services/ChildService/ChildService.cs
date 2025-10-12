using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Data;
using Tibaks_Backend.Models;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.DTOs.Request;

namespace Tibaks_Backend.Services
{
    public class ChildService : IChildService
    {
        private readonly ApplicationDbContext _context;

        public ChildService(ApplicationDbContext context)
        {
            _context = context;
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
    }
}
