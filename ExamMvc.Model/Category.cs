using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Model
{
    public class Category
    {
        private ICollection<Ticket> tickets;
        public Category()
        {
            this.tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }

    }
}
