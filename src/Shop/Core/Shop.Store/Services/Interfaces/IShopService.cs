using Shop.Common.Models.Domain.Response;
using Shop.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Store.Services.Interfaces
{
    public interface IShopService : IBaseService<Models.DbEntities.Shop>
    {
        
    }
}
