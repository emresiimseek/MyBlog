using MyBlog.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.EntityFramework.Concrete
{
    public class Role : IEntity
    {
        public string RoleName { get; set; }
        public virtual List<UsersRole> UsersRoles { get; set; }
    }
}
