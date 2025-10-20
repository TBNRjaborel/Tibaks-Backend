using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Services;

namespace Tibaks_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VaccinationsController : ControllerBase
    {
        private readonly IVaccinationService _vaccinationService;

        public VaccinationsController(IVaccinationService vaccinationService)
        {
            _vaccinationService = vaccinationService;
        }

        // GET: api/vaccinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccinationDto>>> GetAll([FromQuery] string? childId)
        {
            IEnumerable<VaccinationDto> vaccinations;

            if (!string.IsNullOrEmpty(childId))
            {
                vaccinations = await _vaccinationService.GetByChildIdAsync(childId);
            }
            else
            {
                vaccinations = await _vaccinationService.GetAllAsync();
            }

            return Ok(vaccinations);
        }

        // GET: api/vaccinations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccinationDto>> GetById(string id)
        {
            var vaccination = await _vaccinationService.GetByIdAsync(id);
            if (vaccination == null)
                return NotFound();

            return Ok(vaccination);
        }

        // GET: api/vaccinations/schedules/{childId}
        [HttpGet("schedules/{childId}")]
        public async Task<ActionResult<VaccinationScheduleDto>> GetSchedulesByChildId(string childId)
        {
            var schedules = await _vaccinationService.GetSchedulesByChildIdAsync(childId);
            if (schedules == null)
                return NotFound();

            return Ok(schedules);
        }

        // POST: api/vaccinations
        [HttpPost]
        public async Task<ActionResult<VaccinationDto>> Create([FromBody] VaccinationInputDto dto)
        {
            var created = await _vaccinationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // POST: api/vaccinations/batch
        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<VaccinationDto>>> CreateBatch([FromBody] List<VaccinationInputDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                return BadRequest("No vaccination records provided.");

            var createdList = await _vaccinationService.CreateManyAsync(dtos);
            return Ok(createdList);
        }

        // PUT: api/vaccinations/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<VaccinationDto>> Update(string id, [FromBody] VaccinationInputDto dto)
        {
            try
            {
                var updated = await _vaccinationService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // PUT: api/vaccinations/batch
        [HttpPut("batch")]
        public async Task<ActionResult<IEnumerable<VaccinationDto>>> UpdateBatch([FromBody] List<VaccinationDto> dtos)
        {
            if (dtos == null || !dtos.Any())
                return BadRequest("No vaccination records provided.");

            var updatedList = await _vaccinationService.UpdateManyAsync(dtos);
            return Ok(updatedList);
        }

        // PUT: api/vaccinations/schedules
        [HttpPut("schedules")]
        public async Task<ActionResult<IEnumerable<VaccinationScheduleDto>>> UpdateSchedules([FromBody] VaccinationScheduleInputDto inputDto)
        {
            try
            {
                var updatedSchedules = await _vaccinationService.UpdateSchedulesAsync(inputDto);
                return Ok(updatedSchedules);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/vaccinations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _vaccinationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
