using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_opinion")]
    public class RfOpinionEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:意见
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "opinion")]
        public string Opinion { get; set; }

        /// <summary>
        /// Desc:联系信息
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "contact_info")]
        public string ContactInfo { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public int CreateTime { get; set; }

    }
}
