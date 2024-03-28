using Shop.Common.Factories;
using Shop.Common.Repositories.Implements;
using Shop.Store.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Store.Repositories.Implements
{
    public class ShopDao : SugarBaseDao<Models.DbEntities.Shop>, IShopDao
    {
        public ShopDao(SqlSugarClientFactory sqlSugarClientFactory) : base(sqlSugarClientFactory)
        {
        }

        
    }
}
