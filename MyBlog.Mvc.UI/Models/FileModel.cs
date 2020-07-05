using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Mvc.UI.Models
{
    public class FileModel
    {

        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedUsername { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivateGuid { get; set; }
        public  List<Article> Articles { get; set; }
        public  List<Comment> Comments { get; set; }
        public virtual List<UsersRole> UsersRoles { get; set; }
        public  List<Liked> Likes { get; set; }
        public  HttpPostedFileBase file { get; set; }
    }
}