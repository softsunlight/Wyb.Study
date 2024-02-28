using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.RedPacket.DnEntities
{
    /// <summary>
    /// 红包
    /// </summary>
    [SugarTable("red_packet")]
    public class RedPacket
    {
        /// <summary>
        /// 红包id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 红包总金额，
        /// </summary>
        [SugarColumn(ColumnName = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 红包个数
        /// </summary>
        [SugarColumn(ColumnName = "number")]
        public int Number { get; set; }

        /// <summary>
        /// 剩余红包个数
        /// </summary>
        [SugarColumn(ColumnName = "remain_number")]
        public int RemainNumber { get; set; }

        /// <summary>
        /// 剩余金额
        /// </summary>
        [SugarColumn(ColumnName = "remain_amount")]
        public decimal RemainAmount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        [SugarColumn(ColumnName = "modify_time")]
        public DateTime ModifyTime { get; set; } = DateTime.Now;
    }
}
