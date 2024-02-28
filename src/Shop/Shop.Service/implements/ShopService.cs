using Microsoft.Extensions.Logging;
using Shop.Dao.interfaces;
using Shop.Models.Domain.Request.Shop;
using Shop.Models.Domain.Response;
using Shop.Models.Domain.Response.Shop;
using Shop.Service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.implements
{
    public class ShopService : BaseService<Shop.Models.DbEntities.Shop>, IShopService
    {
        private readonly ILogger<ShopService> _logger;
        private readonly IShopDao _shopDao;

        public ShopService(ISugarBaseDao<Models.DbEntities.Shop> sugarBaseDao, ILogger<ShopService> logger, IShopDao shopDao) : base(sugarBaseDao)
        {
            _logger = logger;
            _shopDao = shopDao;
        }

        public async Task<PageResponse<ShopListQueryItem>> GetListAsync(ShopListQueryRequest shopListQueyRequest)
        {
            if (shopListQueyRequest.PageIndex <= 0)
            {
                shopListQueyRequest.PageIndex = 1;
            }
            if (shopListQueyRequest.PageSize <= 0)
            {
                shopListQueyRequest.PageSize = 10;
            }
            var list = await _shopDao.GetListAsync(shopListQueyRequest);
            var count = await _shopDao.GetCountAsync(shopListQueyRequest);
            return PageResponse<ShopListQueryItem>.Successful(new PageResult<ShopListQueryItem>(list, new Models.Domain.Page(shopListQueyRequest.PageIndex, shopListQueyRequest.PageSize, count)));
        }
    }
}
