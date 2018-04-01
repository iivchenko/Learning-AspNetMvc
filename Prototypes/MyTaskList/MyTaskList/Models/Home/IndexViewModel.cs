using MyTaskList.Models.Pagination;
using System.Collections.Generic;

namespace MyTaskList.Models.Home
{
    public sealed class IndexViewModel
    {
        public IEnumerable<TaskItem> Tasks { get; set; }

        public PageViewModel Pagination { get; set; }

        public string Pattern { get; set; }
    }
}