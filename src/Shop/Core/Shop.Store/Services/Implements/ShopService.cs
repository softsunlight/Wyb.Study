using Microsoft.Extensions.Logging;
using Shop.Common.Repositories.Interfaces;
using Shop.Common.Services.Implements;
using Shop.Store.Repositories.Interfaces;
using Shop.Store.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Store.Services.Implements
{
    public class ShopService : BaseService<Models.DbEntities.Shop>, IShopService
    {
        private readonly ILogger<ShopService> _logger;
        private readonly IShopDao _shopDao;

        public ShopService(ISugarBaseDao<Models.DbEntities.Shop> sugarBaseDao, ILogger<ShopService> logger, IShopDao shopDao) : base(sugarBaseDao)
        {
            _logger = logger;
            _shopDao = shopDao;
        }

    }
}
