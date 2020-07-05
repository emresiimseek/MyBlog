using MyBlog.Business.Abstract;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        public ICommentDal _commentDal { get; set; }
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public List<Comment> GetComment(int? id)
        {
            return _commentDal.GetAll(c => c.Id == id);
        }

        public BusinessLayerResult<Comment> AddComment(Comment comment)
        {
            BusinessLayerResult<Comment> businessLayerResult = new BusinessLayerResult<Comment>();
            businessLayerResult.Result = comment;
            if (comment != null)
            {
                int res = _commentDal.Add(comment);

                if (res > 1)
                {
                    businessLayerResult.AddError(MessagesCodes.UnexpectedError, "Beklenmedik Hata");
                    return businessLayerResult;
                }
                else
                {
                    businessLayerResult.AddError(MessagesCodes.Success, "Kaydınız başarılı bir şekilde oluştu.");
                    return businessLayerResult;
                }
            }
            return businessLayerResult;
        }
        public int getCommentCount(int id)
        {
            return _commentDal.GetCommentCount(id);
        }
    }
}
