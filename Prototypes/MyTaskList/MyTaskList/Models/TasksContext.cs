using System.Data.Entity;

namespace MyTaskList.Models
{
    public sealed class TasksContext : DbContext
    {
        public TasksContext()
            : base("PerformaceDb")
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}