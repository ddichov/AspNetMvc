using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Data
{
    public interface IUowData : IDisposable
    {

        IRepository<Ticket> Tickets { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
