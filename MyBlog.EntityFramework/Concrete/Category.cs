using MyBlog.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.EntityFramework.Concrete
{
    [Table("Categories")]
    public class Category : IEntity
    {
        [Required, StringLength(50)]
        public string Title { get; set; }
        public virtual List<Article> Articles { get; set; }
    }
}
