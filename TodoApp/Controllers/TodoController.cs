using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public static List<Task> Tasks = new List<Task>();
        private static int _id = 1;

        [HttpGet("getTasks")]
        public IActionResult GetTasks()
        {
            if (Tasks.Count == 0)
            {
                return NoContent();
            }
            return Ok(Tasks);
        }

        [HttpPost("addTask")]
        public IActionResult AddTask([FromBody] Task task)
        {
            task.Id = _id++;
            Tasks.Add(task);
            return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
        }

        [HttpPut("updateTask")]
        public IActionResult UpdateTask([FromBody] Task task)
        {
            var taskToUpdate = Tasks.FirstOrDefault(t => t.Id == task.Id);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            taskToUpdate.Title = task.Title;
            taskToUpdate.IsComplete = task.IsComplete;

            return NoContent();
        }

        [HttpDelete("deleteTask/{id}")]
        public IActionResult DeleteTask(int id)
        {
            var taskToDelete = Tasks.FirstOrDefault(t => t.Id == id);
            if (taskToDelete == null)
            {
                return NotFound();
            }

            Tasks.Remove(taskToDelete);
            return NoContent();
        }
    }
}