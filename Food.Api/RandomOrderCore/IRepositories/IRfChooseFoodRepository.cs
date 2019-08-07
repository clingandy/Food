using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfChooseFoodRepository : IRepositoriesBase<RfChooseFoodEntity>
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
        Task<List<RfChooseFoodEntity>> FindRfChooseFoodPageList(RfChooseFoodView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);
    }

}

