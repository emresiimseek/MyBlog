using FrameworkCore.CrossCuttingConcerns.Security.Web;
using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.ViewModel;
using MyBlog.Mvc.UI.Areas.Admin.Filters;
using MyBlog.Mvc.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        public ILoginService _loginService;
        public ICategoryService _categoryService;
        public AdminController(ILoginService loginService, ICategoryService categoryService)
        {
            _loginService = loginService;
            _categoryService = categoryService;
        }
        // GET: Admin/Admin
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost, AllowAnonymous]
        public ActionResult Login(LoginUserView model)
        {
            BusinessLayerResult<User> businessLayerResult = _loginService.LoginControl(model.Username, model.Password);
            Session["user"] = businessLayerResult.Result;
            List<UsersRoleses> usersRoles = _loginService.GetRoles(businessLayerResult.Result);
            
            if (ModelState.IsValid)
            {
                if (businessLayerResult.Error.Count > 0)
                {
                    businessLayerResult.Error.ForEach(x => ModelState.AddModelError("", x.Messages));
                    return View(model);
                }
                
                AuthenticationHelper.createCookie(businessLayerResult.Result.Id, businessLayerResult.Result.Username, businessLayerResult.Result.Email, DateTime.Now.AddDays(5), usersRoles, false, businessLayerResult.Result.Name, businessLayerResult.Result.Lastname);
                
                return RedirectToAction("Home", "Admin");

            }
            return View(model);
        }
        [CustomAuthorizeAttribute(usersRole: "Admin")]
        public ActionResult Home()
        {
            Session["cat"] = _categoryService.GetCategories();
            UsersOfViews usersOfViews = new UsersOfViews();
            usersOfViews.users = _loginService.GetUsers();
            return View(usersOfViews);
        }
    }
}