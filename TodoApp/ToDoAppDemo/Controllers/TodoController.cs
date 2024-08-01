using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApp.Application;
using TodoApp.Domain;

namespace TodoApp.ToDoAppDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITaskService _todoService;

        public TodoController(ITaskService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet("getTasks")]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _todoService.GetTasksAsync();
            if (tasks == null || !tasks.Any())
            {
                return NoContent();
            }
            return Ok(tasks);
        }

        [HttpPost("addTask")]
        public async Task<IActionResult> AddTask([FromBody] CustomTask task)
        {
            var addedTask = await _todoService.AddTaskAsync(task);
            return CreatedAtAction(nameof(GetTasks), new { id = addedTask.Id }, addedTask);
        }

        [HttpPut("updateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] CustomTask task)
        {
            var isUpdated = await _todoService.UpdateTaskAsync(task);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var isDeleted = await _todoService.DeleteTaskAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}