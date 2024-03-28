using Shop.Common.Models.DbEntities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.User.Models.DbEntities
{
    [SugarTable("delivery_info")]
    public class DeliveryInfo : BaseEntity
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [SugarColumn(ColumnName = "user_id")]
        public string UserId { get; set; }

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

        /// <summary>
        /// 是否默认地址
        /// </summary>
        [SugarColumn(ColumnName = "is_default")]
        public bool IsDefault { get; set; } = false;
    }
}
