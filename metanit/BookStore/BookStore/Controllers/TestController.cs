using System.Net.Mime;
using System.Text;
using System.Web.Mvc;
using BookStore.Utils;

namespace BookStore.Controllers
{
    public class TestController : Controller
    {
        [HttpGet]
        public string SayHello(string name, int times)
        {
            ViewBag
            var hello = new StringBuilder();

            for(var i = 0; i < times; i++)
            {
                hello.AppendLine($"<p>Hello {name}</p>");
            }

            return hello.ToString();
        }

        [HttpGet]
        public ContentResult SayHello2(string name, int times)
        {
            var hello = new StringBuilder();

            for (var i = 0; i < times; i++)
            {
                hello.AppendLine($"<p>Hello {name}</p>");
            }

            return Content(hello.ToString());
        }

        [HttpGet]
        public ActionResult MyActionResult(string name)
        {
            return new MyHtmlResult(name);
        }

        [HttpGet]
        public FileResult GetFile(string path)
        {
            return new FilePathResult(path, "application/octet-stream");
        }
    }
}