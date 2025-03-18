using Microsoft.AspNetCore.Mvc;
using MyWorkDeskHelpers.Server.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/user-contacts")]
    public class UserContactController : ControllerBase
    {
        private readonly IUserContactService _contactService;

        public UserContactController(IUserContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserContact([FromBody] UserContactInfo contact)
        {
            var createdContact = await _contactService.CreateUserContactAsync(contact);
            return Ok(createdContact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserContact(string id, [FromBody] UserContactInfo contact)
        {
            var updated = await _contactService.UpdateUserContactAsync(id, contact);
            if (!updated) return NotFound();
            return Ok("Контакт обновлен");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserContact(string id)
        {
            var deleted = await _contactService.DeleteUserContactAsync(id);
            if (!deleted) return NotFound();
            return Ok("Контакт удален");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserContactInfo(string userId)
        {
            var contactInfo = await _contactService.GetUserContactByIdAsync(userId);
            if (contactInfo == null)
                return NotFound("Контактные данные не найдены.");

            return Ok(contactInfo);
        }
    }
}
