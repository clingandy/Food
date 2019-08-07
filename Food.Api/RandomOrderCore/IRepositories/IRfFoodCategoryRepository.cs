using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfFoodCategoryRepository : IRepositoriesBase<RfFoodCategoryEntity>
    {
        /// <summary>
        /// 获取餐饮店列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<RfFoodCategoryEntity>> FindRfFoodCategoryPageList(RfFoodCategoryView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);

    }

}

