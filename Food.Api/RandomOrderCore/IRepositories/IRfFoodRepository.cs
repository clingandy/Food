using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfFoodRepository : IRepositoriesBase<RfFoodEntity>
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
        Task<List<RfFoodEntity>> FindRfFoodPageList(RfFoodView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 按条件随机返回第一个食物ID
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<int> FindRfFoodByRand(RfFoodView search);
    }

}

