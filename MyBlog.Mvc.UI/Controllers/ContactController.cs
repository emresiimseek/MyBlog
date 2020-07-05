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
                bool result = false;
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.UserName = "emresimseka@gmail.com";
                WebMail.Password = "dYorK5sr";
                WebMail.EnableSsl = true;

                try
                {
                    WebMail.
                        Send(
                        to: "emresimseka@gmail.com", subject: mailOfView.subject,
                        body: mailOfView.body + " " + mailOfView.toMail+"|"+ mailOfView.Name +','+ "tarafından gönderildi."
                        );
                    result = true;
                    ViewBag.res = "E-Postanız başarılı bir şekilde gönderilmiştir.";

                }
                catch (Exception)
                {

                    ViewBag.res = "Bir hata alındı.";
                }

                ViewBag.result = result;
            }


            return View();
        }


    }
}