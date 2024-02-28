using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Domain.Request.Shop
{
    public class ShopListQueryRequest : Page
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }
    }
}
