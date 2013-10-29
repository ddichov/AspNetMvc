using ExamMvc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamMvc.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (this.HttpContext.Cache["HomePageIndex"] == null)
            {
                var ticketsList = this.Data
                    .Tickets
                    .All()
                    .OrderByDescending(x => x.Comments.Count())
                    .Take(6)
                    .Select(y => new TicketModel
                    {
                        Id = y.Id,
                        Title = y.Title,
                        CategoryName = y.Category.Name,
                        AuthorName = y.Author.UserName,
                        CommentsNumber = y.Comments.Count(),
                    });
                this.HttpContext.Cache.Add("HomePageIndex", ticketsList.ToList(), null, DateTime.Now.AddHours(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.Default, null);
            }
            return View(this.HttpContext.Cache["HomePageIndex"]);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}