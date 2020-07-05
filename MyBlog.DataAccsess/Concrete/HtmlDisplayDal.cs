using FrameworkCore.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Concrete
{
    public class HtmlDisplayDal : EntityRepositoryBase<Html_Content_Result, MyBlogContext>, IHtmlDisplayDal
    {
    }
}
