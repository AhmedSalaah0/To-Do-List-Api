using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Data
{
    public class ToDoTask
    {
        public int ToDoId { get; set; }
        public int TaskId { get; set; }
        public string? UserId { get; set; }
        public string Content { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
