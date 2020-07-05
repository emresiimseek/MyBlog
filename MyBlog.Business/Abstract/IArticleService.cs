using MyBlog.Business.Concrete;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface IArticleService
    {
        List<Article> GetArticles();
        Article GetArticle(int? id);
        List<Category> GetCategories();
        List<Article> GetArticlesByCategory(int? id);
        List<Article> FindInArticles(string word);
        BusinessLayerResult<Article> Add(BusinessLayerResult<Article> businessLayerResult);
        List<Article> GetLastArticle();
        int ImageCounter();
    }
}
