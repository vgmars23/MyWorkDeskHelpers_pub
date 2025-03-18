using Microsoft.AspNetCore.Mvc;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/birthdays")]
    public class BirthdayController : ControllerBase
    {
        private readonly IBirthdayService _birthdayService;

        public BirthdayController(IBirthdayService birthdayService)
        {
            _birthdayService = birthdayService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Birthday>>> GetAll()
        {
            return Ok(await _birthdayService.GetAllBirthdaysAsync());
        }

        [HttpGet("today")] 
        public async Task<IActionResult> GetTodayBirthdays()
        {
            var birthdays = await _birthdayService.GetTodayBirthdaysAsync();

            if (birthdays == null || birthdays.Count == 0)
                return NotFound("Сегодня нет дней рождений");

            return Ok(birthdays);
        }

        [HttpPost]
        public async Task<ActionResult<Birthday>> Create([FromBody] Birthday birthday)
        {
            var createdBirthday = await _birthdayService.CreateBirthdayAsync(birthday);
            return CreatedAtAction(nameof(GetAll), new { id = createdBirthday.Id }, createdBirthday);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBirthday(string id, [FromBody] Birthday birthday)
        {
            if (id != birthday.Id) return BadRequest("ID в URL и объекте не совпадают");

            var updatedBirthday = await _birthdayService.UpdateBirthdayAsync(id, birthday);
            if (updatedBirthday == null) return NotFound();

            return Ok(updatedBirthday);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBirthday(string id)
        {
            var deleted = await _birthdayService.DeleteBirthdayAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
