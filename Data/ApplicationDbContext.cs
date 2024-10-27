using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoListApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MainTask> Tasks { get; set; }
        public DbSet<ToDoTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            // Configure MainTask entity
            modelBuilder.Entity<MainTask>()
                .HasKey(mt => mt.TaskId);

            // Configure ToDoTask entity
            modelBuilder.Entity<ToDoTask>()
                .HasKey(ut => ut.ToDoId);

            modelBuilder.Entity<ToDoTask>()
                .HasOne<MainTask>()
                .WithMany() 
                .HasForeignKey(ut => ut.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
