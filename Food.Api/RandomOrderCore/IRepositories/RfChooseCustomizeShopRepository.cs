using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfChooseCustomizeShopRepository : IRepositoriesBase<RfChooseCustomizeShopEntity>
    {
        #region 随机选择的店铺

        /// <summary>
        /// 查询随机选择的店铺信息
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        Task<List<RfChooseCustomizeShopEntity>> FindRfChooseCustomizeShopPageList(string openId, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        #endregion

    }
}


