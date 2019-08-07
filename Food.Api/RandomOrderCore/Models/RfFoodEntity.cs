using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_food")]
    public class RfFoodEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "food_category_id")]
        public int FoodCategoryId { get; set; }

        /// <summary>
        /// Desc:食物名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:食物图片
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        /// Desc:权重 越大越前
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "sort")]
        public int Sort { get; set; }

        /// <summary>
        /// Desc:0不可用 1可用
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "status")]
        public int Status { get; set; }

        /// <summary>
        /// Desc:描述
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Desc:详细
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "details")]
        public string Details { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "modify_time")]
        public int ModifyTime { get; set; }

    }
}
