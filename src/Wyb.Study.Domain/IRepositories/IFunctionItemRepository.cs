using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Requests.FunctionItem;
using Wyb.Study.Domain.DbEntities;

namespace Wyb.Study.Domain.IRepositories
{
    public interface IFunctionItemRepository
    {
        Task<int> CreateAsync(FunctionItem item);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(FunctionItem item);

        Task<FunctionItem> GetAsync(long id);

        Task<List<FunctionItem>> GetListAsync(GetFunctionItemListRequest request);

        Task<long> GetCountAsync(GetFunctionItemListRequest request);

        Task<FunctionItem> GetAsync(string functionName, string functionUrl);
    }
}
