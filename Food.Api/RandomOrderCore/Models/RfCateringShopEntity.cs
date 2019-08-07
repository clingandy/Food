using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_catering_shop")]
    public class RfCateringShopEntity
    {
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
        public ulong Id { get; set; }

        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string IdStr { get; set; }

        /// <summary>
        /// Desc:店面名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Desc:类别
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "category")]
        public string Category { get; set; }

        /// <summary>
        /// Desc:类型编号
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "type")]
        public int Type { get; set; }

        /// <summary>
        /// Desc:详细地址
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// Desc:地址编码
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "adcode")]
        public int Adcode { get; set; }

        /// <summary>
        /// Desc:省份
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "province")]
        public string Province { get; set; }

        /// <summary>
        /// Desc:城市
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "city")]
        public string City { get; set; }

        /// <summary>
        /// Desc:区域
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "district")]
        public string District { get; set; }

        /// <summary>
        /// Desc:纬度
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Desc:经度
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Desc:0不可用 1可用
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public int CreateTime { get; set; }

    }
}
