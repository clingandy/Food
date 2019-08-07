using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfMemberCustomizeShopRepository : IRepositoriesBase<RfMemberCustomizeShopEntity>
    {
        #region 用户自定义店铺

        /// <summary>
        /// 查询用户自定义店铺信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        Task<List<RfMemberCustomizeShopEntity>> FindRfMemberCustomizeShopPageList(RfMemberCustomizeShopView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);

        #endregion

    }
}


