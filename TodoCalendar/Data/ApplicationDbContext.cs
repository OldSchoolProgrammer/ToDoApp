using Microsoft.EntityFrameworkCore;
using TodoCalendar.Models;

namespace TodoCalendar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TodoTask> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<TodoTask>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed default categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Work", Color = "#007bff" },
                new Category { Id = 2, Name = "Personal", Color = "#28a745" },
                new Category { Id = 3, Name = "Shopping", Color = "#ffc107" },
                new Category { Id = 4, Name = "Health", Color = "#dc3545" },
                new Category { Id = 5, Name = "Learning", Color = "#6f42c1" }
            );

            // Seed sample tasks
            modelBuilder.Entity<TodoTask>().HasData(
                new TodoTask
                {
                    Id = 1,
                    Title = "Welcome to Todo Calendar!",
                    Description = "This is a sample task. Click edit to modify or delete it.",
                    DueDate = DateTime.Now.AddDays(3),
                    Priority = 2,
                    IsCompleted = false,
                    CategoryId = 1,
                    Tags = "demo,welcome",
                    CreatedDate = DateTime.Now
                },
                new TodoTask
                {
                    Id = 2,
                    Title = "Check out the calendar view",
                    Description = "Navigate to the calendar to see your tasks organized by due date.",
                    DueDate = DateTime.Now.AddDays(7),
                    Priority = 1,
                    IsCompleted = false,
                    CategoryId = 2,
                    Tags = "demo",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}
