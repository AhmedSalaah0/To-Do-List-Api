using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ToDoListApi.Data;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TasksController(ApplicationDbContext _context) : ControllerBase
    {

        // Fetshing Data from Main Tasks HeadLine
        [HttpGet]
        //[Route("GetTask")]
        public async Task<IActionResult> GetUserTasks()
        {
            // Retrieve the user ID from the claims
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Fetch tasks associated with this user
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            if (tasks == null || tasks.IsNullOrEmpty())
            {
                return NotFound("User Haven't Any Tasks");
            }

            return Ok(tasks);
        }

        // Adding Data from Main Tasks HeadLine

        [HttpPost]
        //[Route("AddTask")]

        public async Task<IActionResult> CreateTask([FromBody] CreateTask taskDto)
        {
            try
            {
                if (taskDto == null || string.IsNullOrEmpty(taskDto.Title))
                {
                    return BadRequest("Task is invalid.");
                }

                var newTask = new MainTask
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    Completed = false
                };

                _context.Tasks.Add(newTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUserTasks), new { id = newTask.UserId }, newTask);
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        //Check Or Uncheck Task
        [HttpPut("{taskId}")]
        //[Route("CheckTask")]

        public async Task<IActionResult> IsCompleted(int taskId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId && t.UserId == userId);
            
            if (task == null)
            {
                return NotFound($"Task with ID {taskId} not found.");
            }
            if (task.Completed == false)
            {
                task.Completed = true;
                await _context.SaveChangesAsync();
                return Ok($"Task with ID {taskId} marked as completed.");
            }
            task.Completed = false;
            await _context.SaveChangesAsync();
            return Ok($"Task with ID {taskId} marked as incomplete.");

        }
    }
}
