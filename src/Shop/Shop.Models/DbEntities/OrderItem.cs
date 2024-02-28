using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.DbEntities
{
    /// <summary>
    /// 订单明细
    /// </summary>
    [SugarTable("order_item")]
    public class OrderItem : BaseEntity
    {
        /// <summary>
        /// 主订单id
        /// </summary>
        [SugarColumn(ColumnName = "main_order_id")]
        public long MainOrderId { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [SugarColumn(ColumnName = "commodity_id")]
        public long CommodityId { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [SugarColumn(ColumnName = "num")]
        public int Num { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [SugarColumn(ColumnName = "price")]
        public decimal Price { get; set; }
    }
}
