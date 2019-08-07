using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_member_customize_shop")]
    public class RfMemberCustomizeShopEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:类别 枚举
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Desc:标签 枚举
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "lable")]
        public int Lable { get; set; }

        /// <summary>
        /// Desc:店铺名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Desc:添加时间
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
