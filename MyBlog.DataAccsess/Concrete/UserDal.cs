using FrameworkCore.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Concrete
{
    public class UserDal : EntityRepositoryBase<User, MyBlogContext>, IUserDal
    {
        public List<UsersRoleses> GetUsersRoleses(User user)
        {
            using (MyBlogContext context = new MyBlogContext())
            {
                var result = from r in context.Users
                             join ur in context.UsersRole on r.Id equals ur.User.Id
                             select new UsersRoleses
                             {

                                 Roles = ur.Role.RoleName
                             };
                return result.ToList();
            }

        }
    }
}
