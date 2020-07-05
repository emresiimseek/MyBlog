using MyBlog.EntityFramework.Concrete;
using MyBlog.EntityFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccsess
{
    public class MyBlogContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersRole> UsersRole { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Html_Content_Result> Html_Content_Result { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public MyBlogContext()
        {
           
            Database.SetInitializer(new MyInitilazer());
        }


    }
}
