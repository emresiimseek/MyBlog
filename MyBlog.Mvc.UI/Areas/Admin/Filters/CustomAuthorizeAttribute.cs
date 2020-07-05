using FrameworkCore.CrossCuttingConcerns.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Mvc.UI.Areas.Admin.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        private string _usersRole { get; set; }

        public CustomAuthorizeAttribute(string usersRole)
        {
            _usersRole = usersRole;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool validRole = false;
            if (filterContext.HttpContext.User.Identity.IsAuthenticated == true)
            {
                Identity identity = (Identity)filterContext.HttpContext.User.Identity;
                foreach (var role in identity.Roles)
                {
                    if (role.ToString() == _usersRole)
                    {
                        validRole = true;
                    }
                }
            }

            if (!validRole)
            {
                filterContext.Result = new RedirectResult("/Admin/Admin/Login");
            }
        }
    }
}