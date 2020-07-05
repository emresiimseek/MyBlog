using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    public class CommentsController : Controller
    {

        public ICommentService _commentService { get; set; }
        public IArticleService _articleService { get; set; }
        public CommentsController(ICommentService commentService, IArticleService articleService)
        {
            _commentService = commentService;
            _articleService = articleService;
        }
        // GET: Comments
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult AddComment(Comment comment)
        {
            BusinessLayerResult<Comment> businessLayerResult = new BusinessLayerResult<Comment>();
            if (Session["user"] != null)
            {
                User user = Session["user"] as User;
                comment.UserId = user.Id;
            }
            
            businessLayerResult = _commentService.AddComment(comment);
            if (businessLayerResult.Error.Find(x => x.codes == MessagesCodes.UnexpectedError) != null)
            {
                ViewBag.ErrorMessage = "Kayıt işlemi sırasında beklenmedik bir hata oluştu!";
            };
            return PartialView("_AddCommentPartial",businessLayerResult.Result);
            //TODO:Jsonresult dönerek  validationsumarty yapılacak.
        }

    }
}