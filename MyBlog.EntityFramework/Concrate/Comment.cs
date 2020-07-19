using MyBlog.EntityFramework.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.EntityFramework.Concrete
{
    [Table("Comments")]
    public class Comment : IEntity
    {
        [Required, StringLength(300), DisplayName("Mesaj")]
        public string Text { get; set; }
        public  Article Article { get; set; }
        public  User User { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }

    }
}
