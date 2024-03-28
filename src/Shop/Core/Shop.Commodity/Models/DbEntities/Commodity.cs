using Shop.Common.Models.DbEntities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Commodity.Models.DbEntities
{
    [SugarTable("commodity")]
    public class Commodity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(ColumnName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [SugarColumn(ColumnName = "price")]
        public decimal Price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [SugarColumn(ColumnName = "stock")]
        public int Stock { get; set; }

        /// <summary>
        /// 商品详情
        /// </summary>
        [SugarColumn(ColumnName = "desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 店铺id
        /// </summary>
        [SugarColumn(ColumnName = "shop_id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 商品分类id
        /// </summary>
        [SugarColumn(ColumnName = "cat_id")]
        public long CatId { get; set; }
    }
}
