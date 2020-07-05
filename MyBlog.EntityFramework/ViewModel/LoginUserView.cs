using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.EntityFramework.ViewModel
{
    public class LoginUserView
    {
        [Required, MaxLength(25)]
        public string Username { get; set; }
        [Required, MaxLength(25)]
        public string Password { get; set; }
    }
}
