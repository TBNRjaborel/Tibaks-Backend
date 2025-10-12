namespace Tibaks_Backend.DTOs.Response
{
    public class VaccineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RecommendedDosage { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Route { get; set; } = string.Empty;
        public string SiteOfAdministration { get; set; } = string.Empty;
    }
}
