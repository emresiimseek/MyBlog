using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Mvc.UI.Areas.Admin.Models
{
    public class UsersOfViews
    {
        public List<User> users { get; set; }
    }
}