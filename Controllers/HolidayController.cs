using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tibaks_Backend.DTOs.Request;
using Tibaks_Backend.DTOs.Response;
using Tibaks_Backend.Services;

namespace Tibaks_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HolidayDto>> GetHolidayById(int id)
        {
            var holiday = await _holidayService.GetHolidayById(id);
            if (holiday == null)
                return NotFound();
            return Ok(holiday);
        }

        [HttpPost]
        public async Task<ActionResult<HolidayDto>> CreateHoliday(HolidayInputDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var holiday = await _holidayService.CreateHolidayAsync(dto);
            return CreatedAtAction(nameof(GetHolidayById), new { id = holiday.Id }, holiday);
        }

        
    }
}
