using MyBlog.Business.Abstract;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public List<Category> GetCategories()
        {

            return _categoryDal.GetAll().ToList();

        }
        public int AddCategory(Category category)
        {
            return _categoryDal.Add(category);
        }

        public Category GetCategory(int? id)
        {
            return _categoryDal.Get(c => c.Id == id);
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }

        public int Add(Category category)
        {
            return _categoryDal.Add(category);
        }

        public int Delete(Category category)
        {
            return _categoryDal.Delete(category);
        }
        public List<Category> GetCategoriesWithChild()
        {
            return _categoryDal.GetCategoriesWithChild();
        }

     
    }
}
