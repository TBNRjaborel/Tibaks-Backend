using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tibaks_Backend.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MonthlyTargetController : ControllerBase
    {
        private readonly ITargetService _targetService;
        public MonthlyTargetController(ITargetService targetService)
        {
            _targetService = targetService;
        }
        

        // GET api/<MonthlyTargetController>/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TargetDto>> GetTargetById(int id)
        {
            var target = await _targetService.GetTargetByIdAsync(id);
            if (target == null)
                return NotFound();
            return Ok(target);
        }

        // POST api/<MonthlyTargetController>
        [HttpPost]
        public async Task<ActionResult<TargetDto>> CreateTargetDate([FromBody] TargetDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdTarget = await _targetService.CreateTargetPatientsAsync(dto);
            return CreatedAtAction(nameof(CreateTargetDate), new { id = createdTarget.Id }, createdTarget);
        }

        // PUT api/<MonthlyTargetController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TargetDto>> UpdateTargetDate(int id, [FromBody] TargetDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var updatedTarget = await _targetService.UpdateTargetPatientsAsync(id, dto);
                return Ok(updatedTarget);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}
