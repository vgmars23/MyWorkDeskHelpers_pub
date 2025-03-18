using Microsoft.AspNetCore.Mvc;
using MyWorkDeskHelpers.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

namespace MyWorkDeskHelpers.Server.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Ошибка: Название задачи не может быть пустым.");
            }

        
            await _taskService.CreateTaskAsync(task);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TaskItem updatedTask)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Ошибка: ID задачи отсутствует!");
            }

            var existingTask = await _taskService.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound($"Задача с ID {id} не найдена!");
            }

            updatedTask.Id = id;
            await _taskService.UpdateTaskAsync(id, updatedTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingTask = await _taskService.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
