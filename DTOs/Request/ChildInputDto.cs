using System.ComponentModel.DataAnnotations;
using Tibaks_Backend.Models.Enums;

namespace Tibaks_Backend.DTOs.Request
{
    public class ChildInputDto
    {
        public string? Image { get; set; }
        public ChildInfoDto ChildInfo { get; set; } = new();
        public AddressInfoDto Address { get; set; } = new();
        public MotherInfoDto Mother { get; set; } = new();
        public FatherInfoDto Father { get; set; } = new();
    }

    public class ChildInfoDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string? Suffix { get; set; }
        public Sex Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int BirthOrder { get; set; }
        public string PlaceOfDelivery { get; set; } = string.Empty;
        public string BirthWeight { get; set; } = string.Empty;
        public FeedingType FeedingType { get; set; }
        public DateTime DateReferredForNewbornScreening { get; set; }
        public DateTime DateAssessed { get; set; }
    }

    public class AddressInfoDto
    {
        public string HomeAddress { get; set; } = string.Empty;
        public string? NearestLandmark { get; set; }
    }

    public class MotherInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string TetanusToxoidStatus { get; set; } = string.Empty;
        public string? PhicNumber { get; set; }
    }

    public class FatherInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string? Occupation { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
    }
}
