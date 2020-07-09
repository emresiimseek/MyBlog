using MyBlog.Business.Abstract;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.ViewModel;
using MyBlog.Mvc.UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    //[ExpFilter]
    public class ArticlesController : Controller
    {
        private IArticleService _articleService { get; set; }

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        // GET: Articles
        public ActionResult ArticlesPage()
        {
            List<Article> articles = new List<Article>();
            articles = _articleService.GetArticles();
            return View(articles);
        }
        // GET: Articles
        public ActionResult ArticleDetail(int? id)
        {
            Article article = new Article();
            if (id != null)
            {
                article.Id = (int)id;
                article = _articleService.GetArticle(id);
            }
            return View(article);
        }
        public ActionResult getArticlesByCategory(int? id)
        {
            if (id == null)
            {
                return PartialView("_ListedofArticlesPartial");
            }
            TempData["Articles"]= _articleService.GetArticlesByCategory(id) as List<Article>;
           
            return RedirectToAction("Index","Home");
        }
        public PartialViewResult GetArticles(string veri)
        {

            List<Article> articles = new List<Article>();
            string[] words = veri.Split(' ');
            foreach (string word in words)
            {
                List<Article> listofarticles = _articleService.FindInArticles(word);
                if (listofarticles != null)
                {
                    foreach (Article a in listofarticles)
                    {
                        articles.Add(a);
                    }

                }
                else
                {
                    break;
                }
                ;
            }
            return PartialView("_ListedofArticlesPartial", articles);
        }

        public ActionResult GetArticleById(int? id)
        {

            Article article = _articleService.GetArticle(id);
            return View(article);
        }


        public PartialViewResult GetLastArticle()
        {
            List<Article> article = _articleService.GetLastArticle();
            return PartialView("GetLastArticlePartial", article);
        }

    }
}