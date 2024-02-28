using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wyb.Study.RedPacket.DnEntities
{
    /// <summary>
    /// 抢红包的记录
    /// </summary>
    [SugarTable("red_wars_log")]
    public class RedWarsLog
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, IsIdentity = true)]
        public long Id { get; set; }

        /// <summary>
        /// 红包Id
        /// </summary>
        [SugarColumn(ColumnName = "red_packet_id")]
        public long RedPacketId { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        [SugarColumn(ColumnName = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 抢红包的人
        /// </summary>
        [SugarColumn(ColumnName = "user_id")]
        public string UserId { get; set; } = "";

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
