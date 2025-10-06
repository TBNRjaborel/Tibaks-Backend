namespace Tibaks_Backend.DTOs.Response
{
    public class HealthcareWorkerDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? UserId { get; set; }
        public string Position { get; set; } = null!;
    }
}
