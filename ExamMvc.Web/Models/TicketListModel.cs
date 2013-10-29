using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamMvc.Web.Models
{
    /// <summary>
    ///  for listing tickets
    /// </summary>
    public class TicketListModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public Priority Priority { get; set; }

        //show enum as string 
        public string PriorityName { get { return this.Priority.ToString(); } }

        public int CommentsNumber{ get; set; }
    }
}