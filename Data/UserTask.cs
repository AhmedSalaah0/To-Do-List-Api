namespace ToDoListApi.Data
{
    public class UserTask
    {
        public string UserID { get; set; }
        public int TaskID { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now; 

    }
}
