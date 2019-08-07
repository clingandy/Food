using Microsoft.Extensions.Options;
using RandomOrderCore.Common;
using RandomOrderCore.Config;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class RfChooseFoodRepository : DbContext<RfChooseFoodEntity>, IRfChooseFoodRepository
    {
        public RfChooseFoodRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        /// <summary>
        /// 获取餐饮店列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfChooseFoodEntity>> FindRfChooseFoodPageList(RfChooseFoodView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfChooseFoodEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId == search.OpenId)
                .WhereIF(!search.FoodName.IsNullOrWhiteSpace(), t1 => t1.FoodName.Contains(search.FoodName))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }
    }

}

