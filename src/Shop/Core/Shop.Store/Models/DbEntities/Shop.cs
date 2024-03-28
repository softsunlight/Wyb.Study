using Shop.Common.Models.DbEntities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Store.Models.DbEntities
{
    [SugarTable("shop")]
    public class Shop : BaseEntity
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 店铺LOGO
        /// </summary>
        [SugarColumn(ColumnName = "logo")]
        public string Logo { get; set; }

        /// <summary>
        /// 店铺描述
        /// </summary>
        [SugarColumn(ColumnName = "desc")]
        public string Desc { get; set; }
    }
}
