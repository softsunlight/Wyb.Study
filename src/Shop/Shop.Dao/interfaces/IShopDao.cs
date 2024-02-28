using Shop.Models.Domain.Request.Shop;
using Shop.Models.Domain.Response;
using Shop.Models.Domain.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Dao.interfaces
{
    public interface IShopDao : ISugarBaseDao<Shop.Models.DbEntities.Shop>
    {
        Task<List<ShopListQueryItem>> GetListAsync(ShopListQueryRequest shopListQueyRequest);

        Task<int> GetCountAsync(ShopListQueryRequest shopListQueyRequest);
    }
}
