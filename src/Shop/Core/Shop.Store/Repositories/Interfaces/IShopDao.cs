using Shop.Common.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Store.Repositories.Interfaces
{
    public interface IShopDao : ISugarBaseDao<Models.DbEntities.Shop>
    {
        
    }
}
