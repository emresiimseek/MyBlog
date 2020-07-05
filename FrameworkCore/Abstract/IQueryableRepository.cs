using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkCore.Abstract
{
    public interface IQueryableRepository<T> where T : class, new()
    {
    }
}
