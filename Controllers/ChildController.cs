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
    public class ChildController : ControllerBase
    {
        private readonly IChildService _childService;

        public ChildController(IChildService childService)
        {
            _childService = childService;
        }

        /// <summary>
        /// Create a new child record
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ChildDto>> CreateChild([FromBody] ChildInputDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _childService.CreateChildAsync(dto);
            return CreatedAtAction(nameof(GetChildById), new { id = created.Id }, created);
        }

        /// <summary>
        /// Get all child records
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildDto>>> GetAllChildren()
        {
            var children = await _childService.GetAllChildrenAsync();
            return Ok(children);
        }

        /// <summary>
        /// Get all children with their vaccination status and next scheduled vaccines
        /// </summary>
        [HttpGet("vaccine-status")]
        public async Task<ActionResult<IEnumerable<ChildRecordDto>>> GetChildrenWithVaccineStatus()
        {
            var children = await _childService.GetChildrenWithVaccineStatus();
            return Ok(children);
        }

        /// <summary>
        /// Get a single child by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ChildDto>> GetChildById(string id)
        {
            var child = await _childService.GetChildByIdAsync(id);
            if (child == null)
                return NotFound();

            return Ok(child);
        }

        /// <summary>
        /// Update a child record
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ChildDto>> UpdateChild(string id, [FromBody] ChildInputDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _childService.UpdateChildAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        /// <summary>
        /// Delete a child record by ID
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChild(string id)
        {
            var deleted = await _childService.DeleteChildAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
