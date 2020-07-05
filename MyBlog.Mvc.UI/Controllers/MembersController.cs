using FrameworkCore.Utilities.Mappings;
using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using MyBlog.Mvc.UI.Filters;
using MyBlog.Mvc.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    [ExpFilter]
    public class MembersController : Controller
    {
        User UserOfView = new User();
        private ILoginService _loginService;
        private AutoMapperHelper _autoMapperHelper;
        public MembersController(ILoginService loginService, AutoMapperHelper autoMapperHelper)
        {
            _autoMapperHelper = autoMapperHelper;
            _loginService = loginService;
        }
        // GET: Members
        public ActionResult RegisterUser()
        {

            return View(UserOfView);
        }
        [HttpPost]
        public ActionResult RegisterUser(User model)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<User> businessLayerResult = _loginService.RegisterControl(model);
                if (businessLayerResult.Error.Count > 0)
                {
                    businessLayerResult.Error.ForEach(x => ModelState.AddModelError("", x.Messages));
                    return View(model);
                }
                if (businessLayerResult.Error.Find(x => x.codes == MessagesCodes.ErrorRegister) != null)
                {
                    ViewBag.ErrorMessage = "Kayıt işlemi sırasında beklenmedik bir hata oluştu!";
                };


                ViewBag.Ok = "Hesabınız başarılı bir şekilde oluşturuldu.E-posta adresinize bir aktivasyon mail'i gönderilmiştir.Gönderilen maildeki link ile hesabınızı aktifleştirdikten sonra giriş yapabilirsiniz.Giriş sayfasına yönlendiriliyorsunuz...";
                return View(model);
            }
            return View();

            //TODO:burada kullanıcının role kaydı atılacak
        }

        public JsonResult UserProfile(int model)
        {
            User user = _loginService.Get(model);
            User user2 = new User();
            //user2= _autoMapperHelper.MapToSameType<User, User>(user);

            user2.Id = user.Id;
            user2.Username = user.Username;
            user2.Lastname = user.Lastname;
            user2.Name = user.Name;
            user2.Password = user.Password;
            user2.IsActive = user.IsActive;
            user2.Email = user.Email;
            user2.ModifiedUsername = user.ModifiedUsername;
            user2.Profilephoto = user.Profilephoto;
            return Json(user2, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateUser(User user, HttpPostedFileBase file)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            ModelState.Remove("ActivateGuid");

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }
            User olduser = _loginService.Get(user.Id);
            if (olduser != null)
            {
                if (user.Password!=olduser.Password)
                {
                    olduser.Password = user.Password;
                }
                olduser.Password = _loginService.Decrypt(user.Password);
                olduser.Username = user.Username;
                olduser.Name = user.Name;
                olduser.Lastname = user.Lastname;

            }
            if (file!=null)
            {
                
                string[] filetype = file.ContentType.Split('/');
                string filename = $"userpp_{user.Id}.{filetype[1]}";
                Directory.CreateDirectory(Server.MapPath("~/uploads/Profile"));
                file.SaveAs(Server.MapPath($"~/uploads/Profile/{filename}"));
                olduser.Profilephoto = filename;
               
            }
    
         
            _loginService.UpdateUser(olduser);
           
            return Json(olduser, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JavaScriptResult UpdateUser(User model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
        //    }

        //    if (ModelState.IsValid==true)
        //    {
        //        User user = _loginService.Get(model.Id);
        //        _loginService.UpdateUser(model);
        //        ViewBag.Success = "Kaydınız başar";
        //        return PartialView("UserProfilePartail", user);
        //    }

        //        return PartialView("UserProfilePartail", model);

        //}


        public PartialViewResult Val()
        {
            return PartialView("ValPartial");
        }

        //[HttpPost]
        //public ActionResult FileUpload(FileModel Model)
        //{
        //    if (Request.Files.Count > 0)
        //    {

        //    } 
        //    string DosyaAdi = Model.File.FileName;
        //    Model.File.SaveAs(Server.MapPath("~/Content/Dosyalar/" + DosyaAdi));

        //    return View();
        //}


        public JsonResult UpdateUserWithFile(FileModel user)
        {
           
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList() }, JsonRequestBehavior.AllowGet);
            }

            //_loginService.UpdateUser(user);
            User newuser = _loginService.Get(user.Id);
            return Json(newuser, JsonRequestBehavior.AllowGet);

        }



    }
}