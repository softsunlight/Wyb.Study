using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.DbEntities
{
    /// <summary>
    /// 订单
    /// </summary>
    [SugarTable("order")]
    public class Order : BaseEntity
    {
        /// <summary>
        /// 订单总金额
        /// </summary>
        [SugarColumn(ColumnName = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 订单总数量
        /// </summary>
        [SugarColumn(ColumnName = "total_num")]
        public int TotalNum { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact")]
        public string Contact { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [SugarColumn(ColumnName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [SugarColumn(ColumnName = "city")]
        public string City { get; set; }

        /// <summary>
        /// 区/县
        /// </summary>
        [SugarColumn(ColumnName = "district")]
        public string District { get; set; }

        /// <summary>
        /// 街道/镇/乡
        /// </summary>
        [SugarColumn(ColumnName = "street")]
        public string Street { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [SugarColumn(ColumnName = "address")]
        public string Address { get; set; }
    }
}
