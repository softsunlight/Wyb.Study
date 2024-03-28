using CommonLib;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Common.Models.DbEntities
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
        public long Id { get; set; } = SnowflakeHelper.GetId();

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
