using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_choose_food")]
    public class RfChooseFoodEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "id")]
        public long Id { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "open_id")]
        public string OpenId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "food_id")]
        public int FoodId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "food_name")]
        public string FoodName { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public int CreateTime { get; set; }

    }
}
