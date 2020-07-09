using MyBlog.Mvc.UI.Filters;
using MyBlog.Mvc.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Controllers
{
    //[ExpFilter]
    public class AboutController : Controller
    {
      
        public ActionResult about()
        {
            FileModel fileModel = new FileModel();
            fileModel.Id = 1;
            fileModel.Name = "emre";
            return View(fileModel);
        }
    }
}
