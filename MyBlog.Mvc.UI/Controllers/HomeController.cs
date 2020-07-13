using FrameworkCore.Concrete;
using FrameworkCore.Helpers;
using MyBlog.Business.Abstract;
using MyBlog.DataAccsess;
using MyBlog.EntityFramework.Concrete;
using MyBlog.Mvc.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    //[ExpFilter]
    public class HomeController : Controller
    {
        private IArticleService _articleService;
        private ICategoryService _categoryService;
        public HomeController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            if (TempData["Articles"] != null)
            {
                List<Article> artics = new List<Article>();
                artics = TempData["Articles"] as List<Article>;
                return View(artics);
            }
            List<Category> category = _categoryService.GetCategoriesWithChild();
            Session["cat"] = category;
            List<Article> articles = _articleService.GetArticles();
            return View(articles);
        }
        public PartialViewResult GetArticlesWithBanner()
        {
            List<Article> articles = _articleService.GetArticles().ToList();
            return PartialView("_ArticleBannerPartial", articles);
        }




    }
}