using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tibaks_Backend.Models.Enums;

namespace Tibaks_Backend.Models
{
    public class Child
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(100)]
        public string? Image { get; set; }

        [Required]
        public ChildInfo ChildInfo { get; set; } = new();

        [Required]
        public AddressInfo Address { get; set; } = new();

        [Required]
        public MotherInfo Mother { get; set; } = new();

        [Required]
        public FatherInfo Father { get; set; } = new();

        [Required]
        public string UpdatedBy { get; set; } = string.Empty;

        public DateOnly UpdatedAt { get; set; }
    }

    // Child-specific info
    [Owned]
    public class ChildInfo
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Suffix { get; set; }

        [Required]
        public Sex Sex { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public int BirthOrder { get; set; }

        [Required, MaxLength(200)]
        public string PlaceOfDelivery { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string BirthWeight { get; set; } = string.Empty;

        [Required]
        public FeedingType FeedingType { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly DateReferredForNewbornScreening { get; set; }

        [Required, DataType(DataType.Date)]
        public DateOnly DateAssessed { get; set; }
    }

    // Address info
    [Owned]
    public class AddressInfo
    {
        [Required, MaxLength(250)]
        public string HomeAddress { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? NearestLandmark { get; set; }
    }

    // Mother info
    [Owned]
    public class MotherInfo
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [MaxLength(100)]
        public string? Occupation { get; set; }

        [Required, Phone, MaxLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string TetanusToxoidStatus { get; set; } = string.Empty;
        [Required]
        public DateOnly TetanusToxoidDate { get; set; }



        [Required, MaxLength(50)]
        public string? PhicNumber { get; set; }
    }

    // Father info
    [Owned]
    public class FatherInfo
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [MaxLength(100)]
        public string? Occupation { get; set; }

        [Required, Phone, MaxLength(20)]
        public string ContactNumber { get; set; } = string.Empty;
    }
}   
