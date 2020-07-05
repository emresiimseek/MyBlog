using FrameworkCore.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Abstract
{
    public interface ICommentDal : IEntityRepository<Comment>
    {

    }
}
