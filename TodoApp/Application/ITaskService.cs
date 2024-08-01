using TodoApp.Domain;

namespace TodoApp.Application
{
    public interface ITaskService
    {
        Task<IEnumerable<CustomTask>> GetTasksAsync();
        Task<CustomTask> AddTaskAsync(CustomTask task);
        Task<bool> UpdateTaskAsync(CustomTask task);
        Task<bool> DeleteTaskAsync(int id);
    }
}
