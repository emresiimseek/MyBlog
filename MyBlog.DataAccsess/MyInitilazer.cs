using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess
{
    public class MyInitilazer : CreateDatabaseIfNotExists<MyBlogContext>
    {
        protected override void Seed(MyBlogContext context)
        {
        }
    }
}
