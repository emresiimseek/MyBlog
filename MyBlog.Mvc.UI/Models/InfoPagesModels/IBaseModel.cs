using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Mvc.UI.Models.InfoPagesModels
{
    public interface IBaseModel<T>
    {
        List<T> Items { get; set; }
        string Header { get; set; }
        string Url { get; set; }
    }
}
