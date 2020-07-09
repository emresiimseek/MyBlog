using FrameworkCore.Helpers;
using MyBlog.Business.Abstract;
using MyBlog.Mvc.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    public class ContactController : Controller
    {
        private IArticleService _articleService;
        public ContactController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        // GET: SendMail
        public ActionResult ContactPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactPage(MailOfView mailOfView)
        {
            if (ModelState.IsValid == true)
            {
                try
                {
                    MailHelper.SendMail(String.Format("{0}</br> {1} tarafından gödnerilmiştir. ", mailOfView.body,mailOfView.from),"info@emresimsek.website", mailOfView.subject,true);
                    ViewBag.res = "E-Postanız başarılı bir şekilde gönderilmiştir.";
                }
                catch (Exception)
                {
                    ViewBag.res = "Bir hata alındı.";
                }
            }
            return View();
        }


    }
}