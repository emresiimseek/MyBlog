using MyBlog.DataAccsess.Abstract;
using MyBlog.EntityFramework.Messages;
using MyBlog.EntityFramework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concrete
{
    public class HtmlDisplay
    {
        private IHtmlDisplayDal _htmlDisplayDal { get; set; }

        public HtmlDisplay(IHtmlDisplayDal htmlDisplayDal)
        {
            _htmlDisplayDal = htmlDisplayDal;
        }
        public List<Html_Content_Result> GetHtml()
        {
            List<Html_Content_Result> html_Content_Resultres = _htmlDisplayDal.GetAll().ToList();
            return html_Content_Resultres;
        }

        public BusinessLayerResult<Html_Content_Result> AddHtml(BusinessLayerResult<Html_Content_Result> LayerResult)
        {
            BusinessLayerResult<Html_Content_Result> businessLayerResult = new BusinessLayerResult<Html_Content_Result>();
            int res = _htmlDisplayDal.Add(LayerResult.Result);
            if (res > 1)
            {
                businessLayerResult.AddError(MessagesCodes.UnexpectedError, "Kayıt sırasında beklenmedik bir hata yaşandı!");
                return businessLayerResult;
            }

            businessLayerResult.AddError(MessagesCodes.Success, "Kaydınız başarılı bir şekilde gerçekleşti!");

            return businessLayerResult;
        }
    }
}
