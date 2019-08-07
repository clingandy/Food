using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_phone_device")]
    public class RfPhoneDeviceEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:设备品牌
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "brand")]
        public string Brand { get; set; }

        /// <summary>
        /// Desc:设备型号
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "model")]
        public string Model { get; set; }

        /// <summary>
        /// Desc:设备像素比
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "pixelRatio")]
        public int PixelRatio { get; set; }

        /// <summary>
        /// Desc:屏幕宽度，单位px
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "screenWidth")]
        public int ScreenWidth { get; set; }

        /// <summary>
        /// Desc:屏幕高度，单位px
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "screenHeight")]
        public int ScreenHeight { get; set; }

        /// <summary>
        /// Desc:微信设置的语言
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "language")]
        public string Language { get; set; }

        /// <summary>
        /// Desc:操作系统及版本
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "system")]
        public string System { get; set; }

        /// <summary>
        /// Desc:微信版本号
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Desc:客户端平台
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Desc:设备性能等级（仅Android小游戏）。取值为：-2 或 0（该设备无法运行小游戏），-1（性能未知），>=1（设备性能值，该值越高，设备性能越好，目前最高不到50）
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "benchmarkLevel")]
        public int BenchmarkLevel { get; set; }

    }
}
