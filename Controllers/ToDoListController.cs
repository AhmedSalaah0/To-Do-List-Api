using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoListApi.Data;
using ToDoListApi.Migrations;

namespace ToDoListApi.Controllers
{
    [ApiController]
    [Route("Api/")]
    public class ToDoListController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet("GetToDoList/{taskId}")]

        public async Task<IActionResult> GetToDoList(int taskId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tasks = await _context.UserTasks
            .Where(t => t.TaskId == taskId && t.UserId == UserId)
            .ToListAsync();

            if (!tasks.Any())
            {
                return NotFound("No To-Do items found for this Task.");
            }

            return Ok(tasks);
        }


        [HttpPost]
        [Route("AddToDoList")]
        
        public async Task<IActionResult> SendToDo([FromBody] ToDoTask userTask)
        {
            // Retrieve the UserId from the claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a new ToDoTask object
            var newToDo = new ToDoTask
            {
                TaskId = userTask.TaskId,
                UserId = userId, // Set the UserId from claims
                Content = userTask.Content,
                IsCompleted = false
            };

            // Add the new task to the context
            await _context.UserTasks.AddAsync(newToDo);

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok("Task Added");
        }

        [HttpPut("Completed/{ToDoId}")]
        
        public async Task<IActionResult> IsCompleted(int ToDoId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _context.UserTasks.FirstOrDefaultAsync(t => t.ToDoId == ToDoId && t.UserId == userId);

            if (task == null)
            {
                return NotFound($"ToDo with ID {ToDoId} not found.");
            }
            if (task.IsCompleted == false)
            {
                task.IsCompleted = true;
                await _context.SaveChangesAsync();
                return Ok($"ToDo with ID {ToDoId} marked as completed.");
            }
            task.IsCompleted = false;
            await _context.SaveChangesAsync();
            return Ok($"To-Do List Item with ID {ToDoId} marked as incomplete.");

        }
    }
}
