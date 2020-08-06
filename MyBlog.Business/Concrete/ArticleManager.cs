using FrameworkCore.CrossCuttingConcerns.Caching;
using MyBlog.Business.Abstract;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkCore.Aspect.PostSharp.CacheAspect;
using FrameworkCore.Concrete;
using MyBlog.DataAccsess;
using FrameworkCore.Aspect.PostSharp.LogAspect;

namespace MyBlog.Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        private IArticleDal _articleDal { get; set; }
        private ICategoryDal _categoryDal { get; set; }
        private EntityQueryableRepository<Article,MyBlogContext> _entityQueryableRepository { get; set; }
        public ArticleManager(IArticleDal articleDal, ICategoryDal categoryDal, EntityQueryableRepository<Article, MyBlogContext> entityQueryableRepository)
        {
            _articleDal = articleDal;
            _categoryDal = categoryDal;
            _entityQueryableRepository = entityQueryableRepository;
        }

 
        public List<Article> GetArticles()
        {
            return _articleDal.GetArticlesWithUser();
        }

        public Article GetArticle(int? id)
        {
            return _articleDal.GetArticleWithUser(a => a.Id == id);
        }
        [CacheAspect(typeof(MemoryCacheManager), cacheByMunite: 60)]
        public List<Category> GetCategories()
        {
            return _categoryDal.GetAll();
        }

        public List<Article> GetArticlesByCategory(int? id)
        {
            return _articleDal.GetAll(a => a.CategoryId == id);
        }

        public List<Article> FindInArticles(string word)
        {
            return _articleDal.GetAll(a => a.Title.Contains(word));
        }

        public BusinessLayerResult<Article> Add(BusinessLayerResult<Article> businessLayerResult)
        {
            int res = _articleDal.Add(businessLayerResult.Result);
            if (res > 3)
            {
                businessLayerResult.AddError(MessagesCodes.UnexpectedError, "Kayıt Sırasında beklenmedik bir hata yaşandı");
                return businessLayerResult;
            }
            businessLayerResult.AddError(MessagesCodes.Success, "Kayıt İşlemi başarılı.");
            return businessLayerResult;
        }
        public void Update(Article article)
        {
            _articleDal.Update(article);
        }

        public List<Article> GetLastArticle()
        {
            return _articleDal.GetLastArticle().ToList(); ;
        }

        public int ImageCounter()
        {
            return _articleDal.GetImageCount();
        }

        public void UpdateCommentCount(Article article)
        {
            _articleDal.Update(article);
        }
    }
}
