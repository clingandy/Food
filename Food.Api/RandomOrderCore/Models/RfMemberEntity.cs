using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_member")]
    public class RfMemberEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:微信openid
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:用户名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "nick_name")]
        public string NickName { get; set; }

        /// <summary>
        /// Desc:头像
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Desc:性别 1男 0女
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "gender")]
        public int Gender { get; set; }

        /// <summary>
        /// Desc:国家
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Desc:省
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// Desc:市
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Desc:登录次数
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "login_count")]
        public int LoginCount { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public int CreateTime { get; set; }

        /// <summary>
        /// Desc:修改时间
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "modify_time")]
        public int ModifyTime { get; set; }

    }
}
