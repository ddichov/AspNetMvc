using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamMvc.Model;
using ExamMvc.Data;
using Kendo.Mvc.UI;
using ExamMvc.Web.Models;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.Extensions;

namespace ExamMvc.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class CategoryController : BaseController
    {
        public ActionResult Index()
        {
            return View("KendoGrid");
        }

        
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, CategoryViewModel category)
        {
            if (category != null)
            {
                if (this.Data.Categories.All().Any(cat => cat.Name.ToLower() == category.Name.ToLower()))
                {
                    ModelState.AddModelError("Name", "Category Name already exists");
                }

                if (ModelState.IsValid)
                {
                    Category categoryEntity = new Category()
                    {
                        Name = category.Name,
                    };

                    this.Data.Categories.Add(categoryEntity);
                    this.Data.SaveChanges();

                    category.Id = categoryEntity.Id;
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                       ModelState.FirstOrDefault(x => x.Value.Errors.Count > 0).Value.Errors.FirstOrDefault().ErrorMessage);
                }
            }
            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete([DataSourceRequest] DataSourceRequest request,
             CategoryViewModel category)
        {
            this.Data.Categories.Delete(category.Id);
            this.Data.SaveChanges();
            return Json(new[] { category }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories([DataSourceRequest] DataSourceRequest request)
        {
            var categories = this.Data.Categories.All().Select(CategoryViewModel.FromCategory);
            return Json(categories.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update([DataSourceRequest] DataSourceRequest request,
            CategoryViewModel categoryModel)
        {
            if (categoryModel != null)
            {
                if (this.Data.Categories.All()
                    .Any(cat => cat.Name.ToLower() == categoryModel.Name.ToLower()
                    && cat.Id != categoryModel.Id))
                {
                    ModelState.AddModelError("Name", "Category Name already exists");
                }

                var categoryEntity = this.Data.Categories.GetById(categoryModel.Id);

                if (categoryEntity == null)
                {
                    ModelState.AddModelError("CategoryId", "Invalid Category");
                }

                if (ModelState.IsValid)
                {
                    categoryEntity.Name = categoryModel.Name;
                    this.Data.Categories.Update(categoryEntity);
                    this.Data.SaveChanges();
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest,
                       ModelState.FirstOrDefault(x => x.Value.Errors.Count > 0).Value.Errors.FirstOrDefault().ErrorMessage);
                }
            }

            return Json(new[] { categoryModel }.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}
