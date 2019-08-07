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
    public class RfPhoneDeviceRepository : DbContext<RfPhoneDeviceEntity>, IRfPhoneDeviceRepository
    {
        public RfPhoneDeviceRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) : base(mySqlConnectionConfigs)
        {

        }

        #region 手机设备信息

        /// <summary>
        /// 获取手机设备信息列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfPhoneDeviceEntity>> FindRfPhoneDevicePageList(RfPhoneDeviceView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfPhoneDeviceEntity>()
                .WhereIF(!search.OpenId.IsNullOrWhiteSpace(), t1 => t1.OpenId.Contains(search.OpenId))
                .WhereIF(!search.Brand.IsNullOrWhiteSpace(), t1 => t1.Brand.Contains(search.Brand))
                .WhereIF(!search.Model.IsNullOrWhiteSpace(), t1 => t1.Model.Contains(search.Model))
                .WhereIF(!search.Language.IsNullOrWhiteSpace(), t1 => t1.Language.Contains(search.Language))
                .WhereIF(!search.System.IsNullOrWhiteSpace(), t1 => t1.System.Contains(search.System))
                .WhereIF(!search.Version.IsNullOrWhiteSpace(), t1 => t1.Version.Contains(search.Version))
                .WhereIF(!search.Platform.IsNullOrWhiteSpace(), t1 => t1.Platform.Contains(search.Platform))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        #endregion


    }

}

