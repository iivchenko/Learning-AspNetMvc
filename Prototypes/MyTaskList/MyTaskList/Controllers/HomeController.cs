using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using MyTaskList.Models;

namespace MyTaskList.Controllers
{
    public class HomeController : Controller
    {
        private readonly TasksContext _tasksContext = new TasksContext();

        public ActionResult Index()
        {
            ViewBag.Tasks = _tasksContext.Tasks;

            return View();
        }

        public ActionResult Search(string pattern)
        {
            ViewBag.Tasks = _tasksContext.Tasks.ToList().Where(x => Regex.IsMatch(x.Name, pattern));
            
            return View("Index");
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(string name, TaskItemStatus status)
        {
            _tasksContext.Tasks.Add(new TaskItem {Name = name, Status = status});
            _tasksContext.SaveChanges();

            ViewBag.Tasks = _tasksContext.Tasks;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            @ViewBag.Id = id;
            @ViewBag.Name = _tasksContext.Tasks.Single(x => x.Id == id).Name;
            @ViewBag.Status = _tasksContext.Tasks.Single(x => x.Id == id).Status;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(long id, string name, TaskItemStatus status)
        {
            var task = _tasksContext.Tasks.Single(x => x.Id == id);

            task.Name = name;
            task.Status = status;

            _tasksContext.SaveChanges();

            ViewBag.Tasks = _tasksContext.Tasks;

            return RedirectToAction("Index");
        }
    }
}