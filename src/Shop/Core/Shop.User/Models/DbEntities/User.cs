using Shop.Common.Models.DbEntities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.User.Models.DbEntities
{
    [SugarTable("user")]
    public class User : BaseEntity
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [SugarColumn(ColumnName = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [SugarColumn(ColumnName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(ColumnName = "nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnName = "password")]
        public string Password { get; set; }

        /// <summary>
        /// 加密盐值
        /// </summary>
        [SugarColumn(ColumnName = "encrypt_salt")]
        public string EncryptSalt { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnName = "head_img")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(ColumnName = "gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(ColumnName = "birthday")]
        public DateTime Birthday { get; set; }
    }
}
