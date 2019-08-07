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
    public class RfFoodCategoryRepository : DbContext<RfFoodCategoryEntity>, IRfFoodCategoryRepository
    {
        public RfFoodCategoryRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
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
        public Task<List<RfFoodCategoryEntity>> FindRfFoodCategoryPageList(RfFoodCategoryView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfFoodCategoryEntity>()
                .WhereIF(!search.Name.IsNullOrWhiteSpace(), t1 => t1.Name.Contains(search.Name))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

    }

}

