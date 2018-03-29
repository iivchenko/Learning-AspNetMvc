using System.Text;
using System.Web.Mvc;

namespace BookStore.Utils
{
    public sealed class MyHtmlResult : ActionResult
    {
        private readonly string _body;

        public MyHtmlResult(string body)
        {
            _body = body;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            var html = new StringBuilder();

            html
                .AppendLine("<!DOCTYPE html><html><head>")
                .AppendLine("<title>Главная страница</title>")
                .AppendLine("<meta charset=utf-8 />")
                .AppendLine("</head> <body bgcolor=\"red\">")
                .AppendLine(_body)
                .AppendLine("</body></html>");

            context
                .HttpContext
                .Response
                .Write(html.ToString());
        }
    }
}