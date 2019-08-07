using System.Collections.Generic;

namespace RandomOrderCore.Domains.View
{
    public class RfFoodView
    {
        /// <summary>
        /// 食物名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public int? Category { get; set; }

        public List<int> CategoryList { get; set; }

        public string OpenId { get; set; }

    }
}
