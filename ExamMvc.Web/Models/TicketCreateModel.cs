using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamMvc.Web.Models
{

    /// <summary>
    /// used to create ticket
    /// </summary>
    public class TicketCreateModel
    {
        [Required]
        [ShouldNotContainBugAttribute(ErrorMessage = "Ticket’s title should not contain the word “bug” in it")]
        public string Title { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //optional
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //optional
        public string ScreenshotUrl { get; set; }
    }
}