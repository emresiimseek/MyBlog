using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Mvc.UI.Models.InfoPagesModels
{
    public class PagesModel : IBaseModel<MessagesObj>
    {
        public List<MessagesObj> Items { get; set; }
        public string Header { get; set; }
        public string Url { get; set; }
    }
}