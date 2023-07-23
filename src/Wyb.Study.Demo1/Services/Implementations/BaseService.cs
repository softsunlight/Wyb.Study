using Wyb.Study.Demo1.Repositories.Interfaces;
using Wyb.Study.Demo1.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.Demo1.Services.Implementations
{
    public class BaseService<T> : IBaseService<T>
    {
        private readonly IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public int Add(T entity)
        {
            return _baseRepository.Add(entity);
        }
    }
}
