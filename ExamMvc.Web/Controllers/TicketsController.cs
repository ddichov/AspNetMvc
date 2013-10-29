using ExamMvc.Model;
using ExamMvc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace ExamMvc.Web.Controllers
{
    public class TicketsController : BaseController
    {
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketDetailsModel ticket = this.Data.Tickets.All()
                .Where(x => x.Id == id)
                .Select(y => new TicketDetailsModel
                {
                    Id = y.Id,
                    Title = y.Title,
                    CategoryName = y.Category.Name,
                    AuthorName = y.Author.UserName,
                    Comments = y.Comments.Select(c => new CommentViewModel { 
                                        Id=c.Id,
                                        AuthorUsername=c.User.UserName,
                                        Content = c.Content,
                                    }).OrderBy(z=>z.Id),
                    Priority = y.Priority,
                    Description = y.Description,
                    ScreenshotUrl = y.ScreenshotUrl,

                }).FirstOrDefault();

            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment(SubmitCommentModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

                this.Data.Comments.Add(new Comment()
                {
                    UserId = userId,
                    Content = commentModel.Comment,
                    TicketId = commentModel.TicketId,
                });

                this.Data.SaveChanges();

                var viewModel = new CommentViewModel { AuthorUsername = username, Content = commentModel.Comment };
                return PartialView("_CommentPartial", viewModel);
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
            }
        }
	}
}