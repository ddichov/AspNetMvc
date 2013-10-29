using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamMvc.Model;
using ExamMvc.Data;
using ExamMvc.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace ExamMvc.Web.Controllers
{
    [Authorize]
    public class TicketsLoggedUserController : BaseController
    {
        private IQueryable<TicketListModel> GetAllTickets()
        {
            var data = this.Data.Tickets.All().Select(x => new TicketListModel()
            {
                Id = x.Id,
                AuthorName = x.Author.UserName,
                CategoryName = x.Category.Name,
                Priority = x.Priority,
                Title = x.Title,
            }).OrderBy(x => x.Id);

            return data;
        }

        public ActionResult KendoList()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name");
            PopulatePriorities();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketCreateModel ticket)
        {
            if (ModelState.IsValid)
            {
                string userId = this.User.Identity.GetUserId();
                ApplicationUser user = this.Data.Users.All().FirstOrDefault(x => x.Id == userId);
                Category cat = this.Data.Categories.All().FirstOrDefault(x => x.Id == ticket.CategoryId);

                Ticket newTicket = new Ticket()
                {
                    Author = user,
                    Category = cat,
                    Priority = ticket.Priority,
                    Title = ticket.Title,
                    ScreenshotUrl = ticket.ScreenshotUrl,
                    Description = ticket.Description,
                };

                this.Data.Tickets.Add(newTicket);
                user.Points += 1;
                this.Data.SaveChanges();

                return RedirectToAction("KendoList");
            }

            ViewBag.CategoryId = new SelectList(this.Data.Categories.All(), "Id", "Name", ticket.CategoryId);
            PopulatePriorities();

            return View(ticket);
        }

        public JsonResult GetTickets([DataSourceRequest] DataSourceRequest request)
        {
            return Json(this.GetAllTickets().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTicketsByCategoryData()
        {
            var result = this.Data.Categories
                .All()
                .Select(x => new SubmitSearchModel
                {
                    Name = x.Name,
                    Id = x.Id,
                });

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(int? catId)
        {
            var result = this.Data.Tickets.All();
            if (catId != null && catId != 0)
            {
                result = result.Where(x => x.CategoryId == catId.Value);
            }

            var endResult = result.Select(x => new TicketListModel
            {
                Id = x.Id,
                AuthorName = x.Author.UserName,
                CategoryName = x.Category.Name,
                Priority = x.Priority,
                Title = x.Title,
            }).OrderBy(x => x.Id);

            return View("SearchResults", endResult);
        }

        private void PopulatePriorities()
        {
            IList<SelectListItem> resultList = new List<SelectListItem>();
            foreach (Priority p in Enum.GetValues(typeof(Priority)))
            {
                SelectListItem item;
               
                    item = new SelectListItem()
                   {
                       Selected = false,
                       Text = p.ToString(),
                       Value = ((int)p).ToString(),
                   };
                resultList.Add(item);
            }

            // set default priority
            resultList.First(x => x.Value == "1").Selected = true;
            ViewBag.Priority = resultList;
        }
    }
}
