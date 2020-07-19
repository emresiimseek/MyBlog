using MyBlog.EntityFramework.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class BusinessLayerResult<T> where T : class
    {
        public T Result { get; set; }
        public List<MessagesObj> Error { get; set; }

        public BusinessLayerResult()
        {
            Error = new List<MessagesObj>();
        }
        public void AddError(MessagesCodes code, string Messsage)
        {
            Error.Add(new MessagesObj() { codes = code, Messages = Messsage });
        }
    }
}
