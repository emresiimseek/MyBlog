using FrameworkCore.Abstract;
using MyBlog.EntityFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Abstract
{
   public interface IHtmlContentDal: IEntityRepository<Html_Content_Result>
    {
    }
}
