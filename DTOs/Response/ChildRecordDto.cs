using Tibaks_Backend.Models.Enums;

namespace Tibaks_Backend.DTOs.Response
{
    public class ChildRecordDto
    {
        
        public string ChildId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }

        public Sex Sex { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Today.Year - DateOfBirth.Year - (DateTime.Today < DateOfBirth.ToDateTime(new TimeOnly()) // birthday this year not yet reached
             .AddYears(DateTime.Today.Year - DateOfBirth.Year) ? 1 : 0);
            }
        }

        public string Status { get; set; } = null!;
        public DateOnly Schedule { get; set; }
        public List<string> VaccineType { get; set; } = null!;
        
    }
}
