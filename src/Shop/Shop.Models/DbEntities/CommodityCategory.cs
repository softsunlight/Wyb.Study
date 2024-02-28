using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models.DbEntities
{
    [SugarTable("commodity_category")]
    public class CommodityCategory : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        [SugarColumn(ColumnName = "cat_name")]
        public string CatName { get; set; }

        /// <summary>
        /// 父级分类id
        /// </summary>
        [SugarColumn(ColumnName = "parent_id")]
        public long ParentId { get; set; }

        /// <summary>
        /// 父级分类名称
        /// </summary>
        [SugarColumn(ColumnName = "parent_cat_name")]
        public string ParentCatName { get; set; }
    }
}
