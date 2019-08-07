using Microsoft.Extensions.Options;
using RandomOrderCore.Common;
using RandomOrderCore.Config;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class RfChooseCustomizeShopRepository : DbContext<RfChooseCustomizeShopEntity>, IRfChooseCustomizeShopRepository
    {
        public RfChooseCustomizeShopRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 随机选择的店铺

        /// <summary>
        /// 获取随机选择的店铺列表
        /// </summary>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfChooseCustomizeShopEntity>> FindRfChooseCustomizeShopPageList(string openId, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfChooseCustomizeShopEntity>()
                .WhereIF(!openId.IsNullOrWhiteSpace(), t1 => t1.OpenId == openId)
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

