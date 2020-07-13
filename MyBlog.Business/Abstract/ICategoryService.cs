using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetCategories();
        Category GetCategory(int? id);
        void Update(Category category);
        int Add(Category category);
        int Delete(Category category);
        List<Category> GetCategoriesWithChild();

    }
}
