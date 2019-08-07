using Microsoft.Extensions.Options;
using RandomOrderCore.Common;
using RandomOrderCore.Config;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class RfChooseShopRepository : DbContext<RfChooseShopEntity>, IRfChooseShopRepository
    {
        public RfChooseShopRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 随机选择的店铺

        /// <summary>
        /// 获取随机选择的店铺列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfChooseShopEntity>> FindRfChooseShopPageList(RfChooseShopView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfChooseShopEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId == search.OpenId)
                .WhereIF(!search.CateringShopTitle.IsNullOrWhiteSpace(), t1 => t1.CateringShopTitle.Contains(search.CateringShopTitle))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

