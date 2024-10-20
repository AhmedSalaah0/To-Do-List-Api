
namespace ToDoListApi.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {

        public DbSet<ToDoTask> Tasks { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserTask>().ToTable("UserTasks").HasKey(o => new { o.UserID, o.TaskID });
            builder.Entity<ToDoTask>().ToTable("Tasks").HasKey(o => o.TaskId);
        }
    }
}
