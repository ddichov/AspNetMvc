using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Model
{
    public class Ticket
    {
        private ICollection<Comment> comments;

        public Ticket()
        {
            this.comments = new HashSet<Comment>();
        }
        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public virtual ApplicationUser Author { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        // default = Medium
        [Required]
        public Priority Priority { get; set; }

        public string ScreenshotUrl { get; set; }

        public string Description { get; set; }

    }
}
