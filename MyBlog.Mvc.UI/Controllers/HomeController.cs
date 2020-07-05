using FrameworkCore.Concrete;
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
    [ExpFilter]
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
            //log4net.ILog log;
            //log4net.Config.XmlConfigurator.Configure();
            //log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            //log.Info("anasayfadır");
            if (ViewBag.Articles!=null)
            {
                List<Article> artics = new List<Article>();
                artics= ViewBag.Articles as List<Article>;
                return View(artics);
            }

            Session["cat"] = _categoryService.GetCategories();
            List<Article> articles = _articleService.GetArticles();
            return View(articles);
        }
        public PartialViewResult GetArticlesWithBanner()
        {
            List<Article> articles= _articleService.GetArticles();
            return PartialView("_ArticleBannerPartial",articles);
        }




    }
}