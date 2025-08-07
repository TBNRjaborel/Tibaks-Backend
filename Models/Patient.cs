namespace Tibaks_Backend.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string MiddleName { get; set; }
        public required string LastName { get; set; }

        public required string Sex { get; set; }
        public required DateOnly DateOfBirth { get; set; }

        public required string PlaceOfBirth { get; set; }
        public string ? CivilStatus_Mother { get; set; }
    }
}
