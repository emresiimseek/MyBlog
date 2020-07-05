using FrameworkCore.CrossCuttingConcerns.Logging.Log4Net;
using FrameworkCore.CrossCuttingConcerns.Security.Web;
using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using MyBlog.EntityFramework.ViewModel;
using MyBlog.Mvc.UI.Filters;
using MyBlog.Mvc.UI.Models.InfoPagesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Mvc.UI.Controllers
{
    [AllowAnonymous]
    [ExpFilter]
    public class LoginController : Controller
    {
        LoginUserView user = new LoginUserView();
        ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        // GET: Login
        public ActionResult LoginPage()
        {
            return View(user);
        }

        [HttpPost]
        public ActionResult LoginPage(LoginUserView model)
        {
            BusinessLayerResult<User> businessLayerResult = _loginService.LoginControl(model.Username, model.Password);
            List<UsersRoleses> usersRoles = _loginService.GetRoles(businessLayerResult.Result);
            if (ModelState.IsValid)
            {
                if (businessLayerResult.Error.Count > 0)
                {
                    businessLayerResult.Error.ForEach(x => ModelState.AddModelError("", x.Messages));
                    return View(model);
                }
                Session["user"] = businessLayerResult.Result;

                Session["id"] = businessLayerResult.Result.Id;
                AuthenticationHelper.createCookie(businessLayerResult.Result.Id, businessLayerResult.Result.Username, businessLayerResult.Result.Email, DateTime.Now.AddDays(5), usersRoles, false, businessLayerResult.Result.Name, businessLayerResult.Result.Lastname);
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }



        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("LoginPage", "Login");
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<User> businessLayerResult = _loginService.ActivateUser(id);
            if (businessLayerResult.Error.Count > 0)
            {
                PagesModel pagesModel = new PagesModel();
                pagesModel.Header = "Info";
                pagesModel.Items = businessLayerResult.Error;
                pagesModel.Url = "/Home/HomePage";
                return View("BaseViewPage", pagesModel);
            }
            else
            {
                PagesModel pagesModel = new PagesModel();
                pagesModel.Header = "Success";
                pagesModel.Items = new List<MessagesObj> { new MessagesObj { codes = MessagesCodes.Success, Messages = "İşlem Başarılı." } };
                pagesModel.Url = "/Home/HomePage";
                return View("BaseViewPage", pagesModel);
            }
        }
        public ActionResult UserActivateOk()
        {

            return View();
        }
        public ActionResult UserActivateCancel()
        {
            List<MessagesObj> errors = new List<MessagesObj>();
            if (TempData.ContainsKey("Errors"))
                errors = TempData["Errors"] as List<MessagesObj>;
            return View(errors);
        }
    }
}