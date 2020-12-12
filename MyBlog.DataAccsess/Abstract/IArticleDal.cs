using FrameworkCore.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Abstract
{
    public interface IArticleDal : IEntityRepository<Article>
    {
        IQueryable<Article> GetLastArticle();
        int GetImageCount();
        Article GetArticleWithUser(Expression<Func<Article, bool>> filter);
        Article GetArticleAllChilds(Expression<Func<Article, bool>> filter);
        List<Article> GetArticlesWithUser(Expression<Func<Article, bool>> filter=null);
        List<Article> GetArticlesAllChilds(Expression<Func<Article, bool>> filter=null);

    }
}
