using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_choose_customize_shop")]
    public class RfChooseCustomizeShopEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Desc:微信openid
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:店铺ID
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "shop_id")]
        public ulong ShopId { get; set; }

        /// <summary>
        /// Desc:店铺名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "shop_title")]
        public string ShopTitle { get; set; }

        /// <summary>
        /// Desc:类别
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Desc:标签
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "lable")]
        public string Lable { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public int CreateTime { get; set; }

    }
}
