using Shop.Models.Domain.Request.Shop;
using Shop.Models.Domain.Response;
using Shop.Models.Domain.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.interfaces
{
    public interface IShopService : IBaseService<Shop.Models.DbEntities.Shop>
    {
        Task<PageResponse<ShopListQueryItem>> GetListAsync(ShopListQueryRequest shopListQueyRequest);
    }
}
