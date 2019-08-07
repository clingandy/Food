using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderCore.IRepositories
{
    public interface IRfOpinionRepository : IRepositoriesBase<RfOpinionEntity>
    {
        #region 意见

        /// <summary>
        /// 查询意见信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        Task<List<RfOpinionEntity>> FindRfOpinionPageList(RfOpinionView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10);


        #endregion

    }
}


