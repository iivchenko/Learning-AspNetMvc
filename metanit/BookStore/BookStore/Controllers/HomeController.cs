using System;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public sealed class HomeController : Controller
    {
        private readonly BookContext _db;

        public HomeController()
        {
            _db = new BookContext();
        }

        public ActionResult Index()
        {
            ViewBag.Books = _db.Books;

            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;

            _db.Purchases.Add(purchase);
            _db.SaveChanges();

            return "Спасибо," + purchase.Person + ", за покупку!";
        }
    }
}