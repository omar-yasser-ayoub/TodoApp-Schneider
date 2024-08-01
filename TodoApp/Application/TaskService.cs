using Microsoft.EntityFrameworkCore;
using TodoApp.Domain;
using TodoApp.Infrastrutcture.Data;

namespace TodoApp.Application
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomTask>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<CustomTask> AddTaskAsync(CustomTask task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> UpdateTaskAsync(CustomTask task)
        {
            var taskToUpdate = await _context.Tasks.FindAsync(task.Id);
            if (taskToUpdate == null)
            {
                return false;
            }

            taskToUpdate.Title = task.Title;
            taskToUpdate.IsComplete = task.IsComplete;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var taskToDelete = await _context.Tasks.FindAsync(id);
            if (taskToDelete == null)
            {
                return false;
            }

            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
