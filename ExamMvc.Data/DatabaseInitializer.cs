using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Data
{
   public class DatabaseInitializer: DbMigrationsConfiguration<ApplicationDbContext>
    {
       public DatabaseInitializer()
       {
           this.AutomaticMigrationsEnabled = true;
           this.AutomaticMigrationDataLossAllowed = true;

       }
       protected override void Seed(ApplicationDbContext context)
       {
           if (context.Tickets.Count() > 0)
           {
               return;
           }

           Random rand = new Random();

           Category sampleCategory = new Category { Name = "Category 1" };
           ApplicationUser user = new ApplicationUser() { UserName = "TestUser1", Points = 10 };
           ApplicationUser user2 = new ApplicationUser() { UserName = "TestUser2", Points = 10 };

           for (int i = 0; i < 10; i++)
           {
               Ticket ticket = new Ticket();
               ticket.Priority = Priority.Medium;
               ticket.ScreenshotUrl = "http://upload.wikimedia.org/wikipedia/commons/5/5c/Firefox_screenshot-404_error_in_Wikipedia.png";
               ticket.Category = sampleCategory;
               ticket.Title = "Ticket N" +i;
               ticket.Description = " Ticket description " + i;
               ticket.Author = user;
               //ticket.RamMemorySize = rand.Next(1, 16);
               //ticket.Weight = 3;

              

               var commentsCount = rand.Next(0, 10);
               for (int j = 0; j < commentsCount; j++)
               {
                   ticket.Comments.Add(new Comment { Content = ("Comment N" +j +" for ticket N"+i), User = user2 });
                   //TODO:  user points++
               }

               context.Tickets.Add(ticket);
           }

           context.SaveChanges();

           base.Seed(context);
       }
       
    }
}
