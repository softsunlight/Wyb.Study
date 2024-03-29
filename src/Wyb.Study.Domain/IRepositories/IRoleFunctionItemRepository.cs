﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wyb.Study.Application.Contracts.Requests.RoleFunctionItem;
using Wyb.Study.Domain.DbEntities;

namespace Wyb.Study.Domain.IRepositories
{
    public interface IRoleFunctionItemRepository
    {
        Task<int> CreateAsync(RoleFunctionItem item);

        Task<int> DeleteAsync(long id);

        Task<int> UpdateAsync(RoleFunctionItem item);

        Task<RoleFunctionItem> GetAsync(long id);

        Task<List<RoleFunctionItem>> GetListAsync(GetRoleFunctionItemListRequest request);

        Task<long> GetCountAsync(GetRoleFunctionItemListRequest request);

        Task<RoleFunctionItem> GetAsync(string roleCode, string functionUrl);
    }
}
