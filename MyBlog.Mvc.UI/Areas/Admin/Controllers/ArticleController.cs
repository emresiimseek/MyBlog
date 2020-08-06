using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.ViewModel;
using MyBlog.Mvc.UI.Areas.Admin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Areas.Admin.Controllers
{
    [CustomAuthorizeAttribute(usersRole: "Admin")]
    public class ArticleController : Controller
    {

        private int _counter;
        public int Imgcounter
        {
            get => _counter++;
            set
            {
                _counter = value;
            }
        }
        public ICategoryService _categoryService { get; set; }
        public IArticleService _articleService { get; set; }
        public IHtmlContentDal _htmlContentDal { get; set; }
        public HtmlDisplay _htmlDisplay { get; set; }
        public ArticleController(HtmlDisplay htmlDisplay, IArticleService articleService, ICategoryService categoryService, IHtmlContentDal htmlContentDal)
        {
            _htmlContentDal = htmlContentDal;
            _categoryService = categoryService;
            _htmlDisplay = htmlDisplay;
            _articleService = articleService;
        }
        // GET: Admin/Article
       
        public ActionResult Index(int? id)
        {
            Session["cat"] = _categoryService.GetCategories();
            if (id != null)
            {
                Article art = _articleService.GetArticle(id);
                return View(art);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(Article model, HttpPostedFileBase ArticleImage)
        {
            BusinessLayerResult<Article> res = new BusinessLayerResult<Article>();
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                res.Result = model;

                if (ArticleImage != null)
                {
                    if ((ArticleImage.ContentType == "image/jpeg") || ArticleImage.ContentType == "image/jpg" || ArticleImage.ContentType == "image/png")
                    {
                        _counter = _articleService.ImageCounter();
                        string filename = $"Article_{Imgcounter}.{ArticleImage.ContentType.Split('/')[1]}";
                        var newImage = new WebImage(ArticleImage.InputStream);
                        newImage.Save(Server.MapPath($"~/Img/ArticlesImages/{filename}"));
                        res.Result.ArticleImageName = filename;

                        var width = newImage.Width;
                        var height = newImage.Height;
                        if (width > height)
                        {
                            var leftRightCrop = (((width - height) / 2) - 30);
                            newImage.Crop(0, leftRightCrop, 0, leftRightCrop);

                        }
                        else if (height > width)
                        {
                            var topBottomCrop = (((height - width) / 2) + 30);
                            newImage.Crop(topBottomCrop, 0, topBottomCrop, 0);
                        }
                        string[] fn = filename.Split('.');
                        newImage.Save(Server.MapPath($"~/Img/ArticlesImages/{fn[0]}_banner_"));
                    }
                }
                User user = Session["user"] as User;
                res.Result.UserId = user.Id;
                res = _articleService.Add(res);
            }

            if (res.Error.Contains(new EntityFramework.Messages.MessagesObj { codes = EntityFramework.Messages.MessagesCodes.UnexpectedError }))
            {
                res.Error.ForEach(x => ModelState.AddModelError("", x.Messages));
                return View(model);
            }
            return this.View(model);
        }
        public ActionResult ListArticles()
        {
            List<Article> articles = _articleService.GetArticles();
            return View(articles);
        }
        public ActionResult ShowHtml()
        {
            HTMLDisplayViewModel htmlDisplayViewModal = new HTMLDisplayViewModel();
            htmlDisplayViewModal.HTMLContentList = _htmlDisplay.GetHtml();

            return View(htmlDisplayViewModal);
        }

        public ActionResult ListAllArticles()
        {
            List<Article> articles = _articleService.GetArticles();


            return View(articles);
        }
        //public ActionResult EditArticle(int? id)
        //{
        //    Session["cat"] = _categoryService.GetCategories();
        //    Article article= _articleService.GetArticle(id);
        //    return View(article);
        //}
        public ActionResult EditArticle(int? id)
        {
            Session["cat"] = _categoryService.GetCategories();
            if (id != null)
            {
                Article art = _articleService.GetArticle(id);
                return View(art);
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditArticle(Article model, HttpPostedFileBase ArticleImage)
        {
            Article art = _articleService.GetArticle(model.Id);
            art.Html_Content_Result.html_content = model.Html_Content_Result.html_content;
            art.Text = model.Text;
            art.Title = model.Title;
            art.CategoryId = model.CategoryId;
            Html_Content_Result html_Content_Result = new Html_Content_Result();
            html_Content_Result.Id = art.Html_Content_Result.Id;
            html_Content_Result.html_content = model.Html_Content_Result.html_content;
            //TO:DO Image Update
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {

                User user = Session["user"] as User;
                art.UserId = user.Id;
                _articleService.Update(art);
                _htmlContentDal.Update(html_Content_Result);
                //_htmlContentDal.Update(art.Html_Content_Result);
            }
            return this.View(model);
        }
    }
}