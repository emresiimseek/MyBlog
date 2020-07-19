using MyBlog.EntityFramework.Abstract;
using MyBlog.EntityFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlog.EntityFramework.Concrete
{
    [Table("Articles")]
    public class Article : IEntity
    {
        [Required, StringLength(60), DisplayName("Başlık")]
        public string Title { get; set; }
        [DisplayName("Kategori")]
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Yazı Görseli")]
        public string ArticleImageName { get; set; }
        [Required, StringLength(2500), DisplayName("Ön İzleme Metini")]
        public string Text { get; set; }
        public int CommentCount { get; set; }
        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        [AllowHtml]
        [Required]
        [Display(Name = "HTML Content")]
        public virtual Html_Content_Result HtmlPage { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Liked> Likes { get; set; }
        


    }
}
