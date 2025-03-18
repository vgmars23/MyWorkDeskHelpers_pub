using MongoDB.Driver;
using MyWorkDeskHelpers.Application.Interfaces;
using MyWorkDeskHelpers.Server.Domain.Entities;

public class TaskService : ITaskService
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskService(IMongoDatabase database)
    {
        _tasks = database.GetCollection<TaskItem>("Tasks");
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _tasks.Find(_ => true).ToListAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(string id)
    {
        return await _tasks.Find(task => task.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateTaskAsync(TaskItem task)
    {
        await _tasks.InsertOneAsync(task);
    }

    public async Task UpdateTaskAsync(string id, TaskItem updatedTask)
    {
        await _tasks.ReplaceOneAsync(task => task.Id == id, updatedTask);
    }

    public async Task DeleteTaskAsync(string id)
    {
        await _tasks.DeleteOneAsync(task => task.Id == id);
    }
}