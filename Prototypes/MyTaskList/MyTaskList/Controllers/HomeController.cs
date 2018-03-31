using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyTaskList.Models;

namespace MyTaskList.Controllers
{
    public class HomeController : Controller
    {
        private readonly TasksContext _tasksContext = new TasksContext();

        public async Task<ActionResult> Index()
        {
            return View(await _tasksContext.Tasks.ToListAsync());
        }

        public async Task<ActionResult> Search(string pattern)
        {
            var tasks =
                await
                _tasksContext
                    .Tasks
                    .Where(x => x.Name.Contains(pattern))
                    .ToListAsync();

            return View("Index", tasks);
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