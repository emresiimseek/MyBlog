using MyBlog.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyBlog.EntityFramework.Concrete
{
    [Table("Users")]
    public class User : IEntity
    {
        [StringLength(25)]
        public string Name { get; set; }
        [StringLength(25)]
        public string Lastname { get; set; }
        [StringLength(25), Required]
        public string Username { get; set; }
        [StringLength(100), Required]
        public string Password { get; set; }
        [StringLength(70), Required]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public virtual List<Article> Articles { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<UsersRole> UsersRoles { get; set; }
        public virtual List<Liked> Likes { get; set; }
        public string Profilephoto { get; set; }
    }
}
