using ExamMvc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExamMvc.Web.Models
{
    public class CategoryViewModel
    {
        public static Expression<Func<Category, CategoryViewModel>> FromCategory
        {
            get
            {
                return cat => new CategoryViewModel()
                {
                    Id = cat.Id,
                    Name = cat.Name
                };
            }
        }

        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}
