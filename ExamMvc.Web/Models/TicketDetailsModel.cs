using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamMvc.Web.Models
{
    //ticket details (title, description, priority, author, screenshot image and category) 
    // Display all comments for the ticket (no paging is required)
    public class TicketDetailsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string AuthorName { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public string CategoryName { get; set; }
        [Required]
        public int CategoryId { get; set; }

        //optional
        public string Description { get; set; }
        //optional
        public string ScreenshotUrl { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}