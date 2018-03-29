using System.Data.Entity;

namespace MyTaskList.Models
{
    public sealed class TasksInitializer : DropCreateDatabaseAlways<TasksContext>
    {
        protected override void Seed(TasksContext db)
        {
            db.Tasks.Add(new TaskItem {Name = "Task 1", Status = TaskItemStatus.Done});
            db.Tasks.Add(new TaskItem {Name = "Task 2", Status = TaskItemStatus.InProgress});

            base.Seed(db);
        }
    }
}