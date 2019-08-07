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
    public class RfOpinionRepository : DbContext<RfOpinionEntity>, IRfOpinionRepository
    {
        public RfOpinionRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 意见

        /// <summary>
        /// 获取意见列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfOpinionEntity>> FindRfOpinionPageList(RfOpinionView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfOpinionEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId.Contains(search.OpenId))
                .WhereIF(!search.Opinion.IsNullOrWhiteSpace(), t1 => t1.Opinion.Contains(search.Opinion))
                .WhereIF(!search.ContactInfo.IsNullOrWhiteSpace(), t1 => t1.ContactInfo.Contains(search.ContactInfo))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

