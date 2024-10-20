using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoListApi.Data
{
    public class ToDoTask
    {
        public int TaskId { get; set; }

        public string UserId { get; set; } 

        public string title { get; set; }
        public string Description { get; set; }
        public bool completed { get; set; }
    }
}
