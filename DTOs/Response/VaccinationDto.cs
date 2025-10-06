using Tibaks_Backend.DTOs.Request;

namespace Tibaks_Backend.DTOs.Response
{
    public class VaccinationDto : VaccinationInputDto
    {
        public string Id { get; set; } = null!;
    }
}
