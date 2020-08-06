using FrameworkCore.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public Article GetArticleWithUser(Expression<Func<Article, bool>> filter)
        {
            using (MyBlogContext context = new MyBlogContext())
            {
                return context.Set<Article>().Include("User").Include("User.Likes").Include("User.Comments").Include("Category").Include("Html_Content_Result").Include("Comments").Include("Likes").SingleOrDefault(filter);
            }
        }
        public List<Article> GetArticlesWithUser(Expression<Func<Article, bool>> filter)
        {
            using (MyBlogContext context = new MyBlogContext())
            {
                return filter == null ?
                          context.Set<Article>().Include("User").Include("Category").Include("Html_Content_Result").Include("Comments").Include("Likes").ToList() :
                          context.Set<Article>().Include("User").Where(filter).ToList();
            };
        }

        public Article GetArticleAllChilds(Expression<Func<Article, bool>> filter)
        {
            MyBlogContext context = new MyBlogContext();
                return context.Set<Article>().Include("User").Include("Category").Include("HtmlPage").Include("Comments").Include("Likes").SingleOrDefault(filter);
        }
    }
}
