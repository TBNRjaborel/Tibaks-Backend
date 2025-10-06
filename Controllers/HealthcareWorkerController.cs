using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Services;

namespace Tibaks_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class HealthcareWorkersController : ControllerBase
    {
        private readonly IHealthcareWorkerService _healthcareWorkerService;

        public HealthcareWorkersController(IHealthcareWorkerService healthcareWorkerService)
        {
            _healthcareWorkerService = healthcareWorkerService;
        }

        // GET: api/healthcareworkers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthcareWorkerDto>>> GetAll()
        {
            var workers = await _healthcareWorkerService.GetAllAsync();
            return Ok(workers);
        }

        // GET: api/healthcareworkers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthcareWorkerDto>> GetById(string id)
        {
            var worker = await _healthcareWorkerService.GetByIdAsync(id);
            if (worker == null)
                return NotFound();

            return Ok(worker);
        }
    }
}
