using MyBlog.Business.Abstract;
using MyBlog.Business.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.DataAccsess.Concrete;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginService>().To<LoginManager>();
            Bind<IUserDal>().To<UserDal>();
            Bind<IUsersRoleDal>().To<UsersRoleDal>();
            Bind<ICategoryDal>().To<CategoryDal>();
            Bind<ICategoryService>().To<CategoryManager>();
            Bind<IArticleDal>().To<ArticleDal>();
            Bind<IArticleService>().To<ArticleManager>();
            Bind<IHtmlDisplayDal>().To<HtmlDisplayDal>();
            Bind<ICommentService>().To<CommentManager>();
            Bind<ICommentDal>().To<CommentDal>();
            Bind<IHtmlContentDal>().To<HtmlContentDal>();

        }
    }
}
