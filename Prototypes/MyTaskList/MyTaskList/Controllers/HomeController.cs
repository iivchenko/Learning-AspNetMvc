using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyTaskList.Models;
using MyTaskList.Models.Home;

namespace MyTaskList.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 20;
        private const int PagesCount = 5;
        private readonly TasksContext _tasksContext = new TasksContext();

        [HttpGet]
        public async Task<ActionResult> Index(string pattern = "", int page = 1)
        {
            var totalPages = (int)Math.Ceiling(_tasksContext.Tasks.Where(x => x.Name.Contains(pattern)).Count() / (decimal)PageSize);

            if (page < 1 || page > totalPages)
            {
                return new HttpNotFoundResult($"Invalid page '{page}'! Should be in range from 1 to {totalPages}");
            }

            var tasks =
                await _tasksContext
                    .Tasks
                    .Where(x => x.Name.Contains(pattern))
                    .OrderBy(x => x.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).ToListAsync();

            var indexVm = new IndexViewModel
            {
                Pagination = new Models.Pagination.PageViewModel(totalPages, PagesCount, page),
                Tasks = tasks,
                Pattern = pattern
            };

            return View("Index", indexVm);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(TaskItem task)
        {
            _tasksContext.Tasks.Add(task);
            await _tasksContext.SaveChangesAsync();

            return RedirectToAction("Index", await _tasksContext.Tasks.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            return View(await _tasksContext.Tasks.SingleAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(TaskItem task)
        {
            _tasksContext.Entry(task).State = EntityState.Modified;

            await _tasksContext.SaveChangesAsync();

            return RedirectToAction("Index", await _tasksContext.Tasks.ToListAsync());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _tasksContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}