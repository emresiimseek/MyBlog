using MyBlog.EntityFramework.Abstract;
using MyBlog.EntityFramework.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlog.EntityFramework.ViewModel
{
    public class Html_Content_Result
    {
        [ForeignKey("Article")]
        public int Id { get; set; }
        [AllowHtml]
        public string html_content { get; set; }
        public virtual Article Article { get; set; }

    }
}
