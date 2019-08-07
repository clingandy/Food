using Microsoft.Extensions.Options;
using RandomOrderCore.Common;
using RandomOrderCore.Config;
using RandomOrderCore.Domains.View;
using RandomOrderCore.Enum;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class RfFoodRepository : DbContext<RfFoodEntity>, IRfFoodRepository
    {
        public RfFoodRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
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
        public Task<List<RfFoodEntity>> FindRfFoodPageList(RfFoodView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfFoodEntity>()
                .Where(t1 => t1.Status == (int)StatusEnum.可用)
                .WhereIF(!search.Name.IsNullOrWhiteSpace(), t1 => t1.Name.Contains(search.Name))
                .WhereIF(search.CategoryList?.Count > 0, t1 => search.CategoryList.Contains(t1.FoodCategoryId))
                .OrderBy(orderByStr)
                .Select("id, name, img_url, description, sort")
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// 按条件随机返回第一个食物ID
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<int> FindRfFoodByRand(RfFoodView search)
        {
            var model = await Db.Queryable<RfFoodEntity>()
                .Where(t1 => t1.Status == (int)StatusEnum.可用)
                .WhereIF(!search.Name.IsNullOrWhiteSpace(), t1 => t1.Name.Contains(search.Name))
                .WhereIF(search.CategoryList?.Count > 0, t1 => search.CategoryList.Contains(t1.FoodCategoryId))
                .OrderBy("rand()")
                .Select("SQL_NO_CACHE id")
                .FirstAsync();
            return model == null ? 0 : model.Id;
        }

    }

}

