using RandomOrderCore.Models;
using System.Collections.Generic;

namespace RandomOrderApi.Cache
{
    public class GlobalStaticCache
    {
        /// <summary>
        /// 美食分类
        /// <para>数据只在后台线程加</para>
        /// </summary>
        public static List<RfFoodCategoryEntity> FoodCategoryList { get; set; } = new List<RfFoodCategoryEntity>();
    }
}
