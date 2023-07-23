using Wyb.Study.Demo1.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Demo1.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        public int Add(T entity)
        {
            System.Console.WriteLine($"{typeof(T).FullName} Call Add Method");
            return 1;
        }
    }
}
