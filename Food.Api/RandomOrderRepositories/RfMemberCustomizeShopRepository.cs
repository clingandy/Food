using Microsoft.Extensions.Options;
using RandomOrderCore.Common;
using RandomOrderCore.Config;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderRepositories
{
    public class RfMemberCustomizeShopRepository : DbContext<RfMemberCustomizeShopEntity>, IRfMemberCustomizeShopRepository
    {
        public RfMemberCustomizeShopRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 用户自定义店铺

        /// <summary>
        /// 获取用户自定义店铺列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfMemberCustomizeShopEntity>> FindRfMemberCustomizeShopPageList(RfMemberCustomizeShopView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfMemberCustomizeShopEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId == search.OpenId)
                .WhereIF(!search.Title.IsNullOrWhiteSpace(), t1 => t1.Title.Contains(search.Title))
                .WhereIF(search.Lable != null && search.Lable.Value > 0, t1 => t1.Lable == search.Lable.Value)
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

