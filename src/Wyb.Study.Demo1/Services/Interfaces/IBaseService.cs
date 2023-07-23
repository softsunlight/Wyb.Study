using Wyb.Study.Demo1.DbEntities;
using Wyb.Study.Demo1.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Demo1.Services.Interfaces
{
    public interface IBaseService<T>
    {
        int Add(T entity);
    }
}
