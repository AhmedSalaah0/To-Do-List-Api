using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListApi.Data;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoListController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserTasks()
        {
            // Retrieve the user ID from the claims
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch tasks associated with this user
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            if (tasks == null || !tasks.Any())
            {
                return NotFound("User Haven't Any Tasks");
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTask taskDto)
        {
            if (taskDto == null || string.IsNullOrEmpty(taskDto.Title))
            {
                return BadRequest("Task is invalid.");
            }

            var newTask = new ToDoTask
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Use authenticated user's ID
                title = taskDto.Title,
                Description = taskDto.Description,
                completed = false
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserTasks), new { id = newTask.UserId }, newTask);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> IsCompleted(int taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId);
            if (task == null)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }
            if (task.completed == false)
            {
                task.completed = true;
                await _context.SaveChangesAsync();
                return Ok($"Task with ID {taskId} marked as completed.");
            }
            task.completed = false;
            await _context.SaveChangesAsync();
            return Ok($"Task with ID {taskId} marked as incomplete.");

        }
    }
}
