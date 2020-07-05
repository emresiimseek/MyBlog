using FrameworkCore.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Concrete
{
    public class ArticleDal : EntityRepositoryBase<Article, MyBlogContext>, IArticleDal
    {
        public int GetImageCount()
        {
            MyBlogContext myBlogContext = new MyBlogContext();
            return myBlogContext.Articles.Count();
        }

        public IQueryable<Article> GetLastArticle()
        {
            MyBlogContext myBlogContext = new MyBlogContext();
            return myBlogContext.Articles.OrderByDescending(x => x.CreatedOn);

        }
    }
}
