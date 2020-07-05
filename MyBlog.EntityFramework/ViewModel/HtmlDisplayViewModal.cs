using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlog.EntityFramework.ViewModel
{
    public class HTMLDisplayViewModel
    {
        [AllowHtml]
        [Required]
        [Display(Name = "HTML Content")]
        public Html_Content_Result HtmlContent { get; set; }

        public List<Html_Content_Result> HTMLContentList { get; set; }
    }
}
