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
    public class RfMemberRepository : DbContext<RfMemberEntity>, IRfMemberRepository
    {
        public RfMemberRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 微信用户信息表

        /// <summary>
        /// 获取微信用户信息表列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfMemberEntity>> FindRfMemberPageList(RfMemberView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfMemberEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId.Contains(search.OpenId))
                .WhereIF(!search.AvatarUrl.IsNullOrWhiteSpace(), t1 => t1.AvatarUrl.Contains(search.AvatarUrl))
                .WhereIF(!search.Country.IsNullOrWhiteSpace(), t1 => t1.Country.Contains(search.Country))
                .WhereIF(!search.Province.IsNullOrWhiteSpace(), t1 => t1.Province.Contains(search.Province))
                .WhereIF(!search.City.IsNullOrWhiteSpace(), t1 => t1.City.Contains(search.City))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

