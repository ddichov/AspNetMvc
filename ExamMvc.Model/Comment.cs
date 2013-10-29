using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
       
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
