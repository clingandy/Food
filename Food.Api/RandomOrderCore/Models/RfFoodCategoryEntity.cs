using System;
using SqlSugar;

namespace RandomOrderCore.Models
{
    [SugarTable("rf_food_category")]
    public class RfFoodCategoryEntity
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Desc:父级ID 顶级为0
        /// Default:0
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "p_id")]
        public int PId { get; set; }

        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Desc:排序
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
        /// Desc:
        /// Default:
        /// Nullable:NO
        /// </summary>
        [SugarColumn(ColumnName = "modify_time")]
        public int ModifyTime { get; set; }

    }
}
