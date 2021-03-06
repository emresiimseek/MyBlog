﻿using FrameworkCore.Concrete;
using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess.Concrete
{
    public class CategoryDal : EntityRepositoryBase<Category, MyBlogContext>, ICategoryDal
    {
        public List<Category> GetCategoriesWithChild()
        {
            using (MyBlogContext context = new MyBlogContext())
            {
                return context.Categories.Include("Articles").ToList();
            };
        }
    }
}
