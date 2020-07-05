using FrameworkCore.Utilities.Comman;
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
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginService>().To<LoginManager>();
            Bind<IUserDal>().To<UserDal>();
            Bind<ILoginService>().ToConstant(WcfPorxy<LoginManager>.createChannel());
        }
    }
}
