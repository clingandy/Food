using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfCateringShopRepository : IRepositoriesBase<RfCateringShopEntity>
    {
        /// <summary>
        /// 查询餐饮店信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        Task<List<RfCateringShopEntity>> FindRfCateringShopPageList(RfCateringShopView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 查询在ID列表中存在的数据的ID
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<ulong>> FindExistId(List<ulong> ids);
    }
}


