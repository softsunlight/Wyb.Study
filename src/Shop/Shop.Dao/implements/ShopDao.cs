using Shop.Dao.interfaces;
using Shop.Models.Domain.Request.Shop;
using Shop.Models.Domain.Response;
using Shop.Models.Domain.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Dao.implements
{
    public class ShopDao : SugarBaseDao<Shop.Models.DbEntities.Shop>, IShopDao
    {
        public ShopDao(SqlSugarClientFactory sqlSugarClientFactory) : base(sqlSugarClientFactory)
        {
        }

        public async Task<List<ShopListQueryItem>> GetListAsync(ShopListQueryRequest shopListQueyRequest)
        {
            return await _sugarSqlClient.Queryable<Shop.Models.DbEntities.Shop>()
                .WhereIF(!string.IsNullOrWhiteSpace(shopListQueyRequest.Name), p => p.Name.Contains(shopListQueyRequest.Name))
                .Skip((shopListQueyRequest.PageIndex - 1) * shopListQueyRequest.PageSize)
                .Take(shopListQueyRequest.PageSize)
                .Select(p => new ShopListQueryItem()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Logo = p.Logo,
                    Desc = p.Desc
                })
                .ToListAsync();
        }

        public async Task<int> GetCountAsync(ShopListQueryRequest shopListQueyRequest)
        {
            return await _sugarSqlClient.Queryable<Shop.Models.DbEntities.Shop>()
                .WhereIF(!string.IsNullOrWhiteSpace(shopListQueyRequest.Name), p => p.Name.Contains(shopListQueyRequest.Name))
                .CountAsync();
        }
    }
}
