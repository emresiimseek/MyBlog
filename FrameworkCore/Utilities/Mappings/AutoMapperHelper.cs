using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace FrameworkCore.Utilities.Mappings
{
   public class AutoMapperHelper
   
    {
        public TDest MapToSameType<TSource,TDest>(TSource obj)
                 where TDest : new()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSource, TDest>());
            IMapper mapper = config.CreateMapper();
            return mapper.Map<TSource,TDest>(obj);
        }
    }
}
