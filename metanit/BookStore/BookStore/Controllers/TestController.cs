using System.Text;
using System.Threading;
using System.Web.Mvc;
using BookStore.Utils;

namespace BookStore.Controllers
{
    public class TestController : Controller
    {

        [HttpGet]
        public ActionResult Hang()
        {
            while (true)
            {
                Thread.Sleep(20000);
            }

            return HttpNotFound("This should not be returned!!");
        }

        [HttpGet]
        public ActionResult OpenPdf()
        {
            var file_path = @"D:\Temp\Eliplane.pdf";
            var file_type = "application/pdf";
            var file_name = "Eliplane.pdf";

            return File(file_path, file_type, file_name);
        }

        [HttpGet]
        public ActionResult NoAccess()
        {
            return new HttpStatusCodeResult(404);
        }

        [HttpGet]
        public ActionResult NoAccess2()
        {
            return new HttpStatusCodeResult(401);
        }

        [HttpGet]
        public string SayHello(string name, int times)
        {
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