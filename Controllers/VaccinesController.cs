using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Services;

namespace Tibaks_Backend.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class VaccinesController : ControllerBase
    {
        private readonly IVaccineService _vaccineService;

        public VaccinesController(IVaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        // GET: api/vaccines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VaccineDto>>> GetAll()
        {
            var vaccines = await _vaccineService.GetAllAsync();
            return Ok(vaccines);
        }

        // GET: api/vaccines/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VaccineDto>> GetById(int id)
        {
            var vaccine = await _vaccineService.GetByIdAsync(id);
            if (vaccine == null)
                return NotFound();

            return Ok(vaccine);
        }
    }
}