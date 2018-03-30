using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
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
            ViewBag.Tasks = await _tasksContext.Tasks.ToListAsync();

            return View();
        }

        public async Task<ActionResult> Search(string pattern)
        {
            // TODO: Think on a better sollution.
            var tasks = await _tasksContext.Tasks.ToListAsync();

            // TODO: Think on regex validation
            ViewBag.Tasks = tasks.Where(x => Regex.IsMatch(x.Name, pattern));
            
            return View("Index");
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(string name, TaskItemStatus status)
        {
            _tasksContext.Tasks.Add(new TaskItem {Name = name, Status = status});
            await _tasksContext.SaveChangesAsync();

            ViewBag.Tasks = await _tasksContext.Tasks.ToListAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            // TODO: Make a better code
            @ViewBag.Id = id;

            var task = await _tasksContext.Tasks.SingleAsync(x => x.Id == id);
            @ViewBag.Name = task.Name;
            @ViewBag.Status = task.Status;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(long id, string name, TaskItemStatus status)
        {
            var task = await _tasksContext.Tasks.SingleAsync(x => x.Id == id);

            task.Name = name;
            task.Status = status;

            await _tasksContext.SaveChangesAsync();

            ViewBag.Tasks = await _tasksContext.Tasks.ToListAsync();

            return RedirectToAction("Index");
        }
    }
}