using MyWorkDeskHelpers.Server.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorkDeskHelpers.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasksAsync();

        Task<TaskItem> GetTaskByIdAsync(string id);

        Task CreateTaskAsync(TaskItem task);

        Task UpdateTaskAsync(string id, TaskItem updatedTask);

        Task DeleteTaskAsync(string id);
    }
}
