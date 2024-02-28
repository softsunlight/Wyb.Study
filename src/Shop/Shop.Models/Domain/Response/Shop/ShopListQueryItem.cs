using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.Domain.Response.Shop
{
    public class ShopListQueryItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 店铺LOGO
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 店铺描述
        /// </summary>
        public string Desc { get; set; }
    }
}
