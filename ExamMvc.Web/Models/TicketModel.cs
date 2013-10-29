using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExamMvc.Web.Models
{
    public class TicketModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string AuthorName { get; set; }

        public string CategoryName { get; set; }

        public int CommentsNumber{ get; set; }
    }
}