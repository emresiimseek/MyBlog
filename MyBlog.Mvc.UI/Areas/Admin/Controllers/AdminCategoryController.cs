using MyBlog.Business.Abstract;
using MyBlog.EntityFramework.Concrete;
using MyBlog.Mvc.UI.Areas.Admin.Filters;
using MyBlog.Mvc.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(usersRole: "Admin")]
    public class AdminCategoryController : Controller
    {
        private Category category = new Category();
        public ICategoryService _categoryService { get; set; }
        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: Admin/Category
        public ActionResult GetByCatId(int? id)
        {
            Category category = _categoryService.GetCategory(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult GetByCatId(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.Update(category);
            }
            return View(category);
        }
        public PartialViewResult Categories()
        {
            Categories categories = new Categories();
            categories.categories = _categoryService.GetCategories();
            return PartialView("_ListOfCatPartial", categories);
        }
        public ActionResult Create()
        {

            return View(category);
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                int result = _categoryService.Add(category);
                if (result == 1)
                {
                    ViewBag.result = "Kaydınız başarılı bir şekilde oluşturulmuştur.";
                }
                else
                {
                    ViewBag.result = "Kaydınız oluştulurken hata alındı.";
                }
            }
            return View(category);
        }
        public JsonResult Delete(int veri)
        {
            Category category = new Category();
            category.Id = veri;
            int result = _categoryService.Delete(category);

            ViewBag.result = "Kayıt silinirken hata alınmıştır.";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}