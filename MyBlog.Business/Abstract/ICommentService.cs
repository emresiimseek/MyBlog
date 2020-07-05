using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface ICommentService
    {
        List<Comment> GetComment(int? id);
        BusinessLayerResult<Comment> AddComment(Comment comment);
    }
}
