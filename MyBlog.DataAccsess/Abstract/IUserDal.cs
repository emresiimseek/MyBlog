using FrameworkCore.Abstract;
using MyBlog.EntityFramework.ComplexTypes;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<UsersRoleses> GetUsersRoleses(User user);
    }
}
