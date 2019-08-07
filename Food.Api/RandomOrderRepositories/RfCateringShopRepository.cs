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
    public class RfCateringShopRepository : DbContext<RfCateringShopEntity>, IRfCateringShopRepository
    {
        public RfCateringShopRepository(IOptions<List<MySqlConnectionConfig>> mySqlConnectionConfigs) :base(mySqlConnectionConfigs)
        {

        }

        #region 餐饮店

        /// <summary>
        /// 获取餐饮店列表
        /// </summary>
        /// <param name="search"></param>
        /// <param name="orderByStr"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<List<RfCateringShopEntity>> FindRfCateringShopPageList(RfCateringShopView search, string orderByStr, ref int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            return Db.Queryable<RfCateringShopEntity>()
                .WhereIF(!search.Title.IsNullOrWhiteSpace(), t1 => t1.Title.Contains(search.Title))
                .WhereIF(!search.Category.IsNullOrWhiteSpace(), t1 => t1.Category.Contains(search.Category))
                .WhereIF(!search.Address.IsNullOrWhiteSpace(), t1 => t1.Address.Contains(search.Address))
                .WhereIF(!search.Province.IsNullOrWhiteSpace(), t1 => t1.Province.Contains(search.Province))
                .WhereIF(!search.City.IsNullOrWhiteSpace(), t1 => t1.City.Contains(search.City))
                .WhereIF(!search.District.IsNullOrWhiteSpace(), t1 => t1.District.Contains(search.District))
                .OrderBy(orderByStr)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
        }

        /// <summary>
        /// 查询在ID列表中存在的数据的ID
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<ulong>> FindExistId(List<ulong> ids)
        {
            return Db.Queryable<RfCateringShopEntity>()
                .Where(t1 => ids.Contains(t1.Id))
                .Select(t=> t.Id)
                .ToListAsync();
        }

        #endregion


    }
}

