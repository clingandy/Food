using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfMemberCustomizeShopServer
    {
        #region 构造

        IRfMemberCustomizeShopRepository _rfMemberCustomizeShopRepository;

        public RfMemberCustomizeShopServer(IRfMemberCustomizeShopRepository rfMemberCustomizeShopRepository)
        {
            _rfMemberCustomizeShopRepository = rfMemberCustomizeShopRepository;
        }

        #endregion

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
        public Task<List<RfMemberCustomizeShopEntity>> GetRfMemberCustomizeShopPageList(RfMemberCustomizeShopView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfMemberCustomizeShopRepository.FindRfMemberCustomizeShopPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取用户自定义店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfMemberCustomizeShopEntity> GetRfMemberCustomizeShop(long id)
        {
            return _rfMemberCustomizeShopRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加用户自定义店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfMemberCustomizeShop(RfMemberCustomizeShopEntity model)
        {
            model.CreateTime = DateTime.Now.D2LSecond();
            return _rfMemberCustomizeShopRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改用户自定义店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfMemberCustomizeShop(RfMemberCustomizeShopEntity model)
        {
            model.ModifyTime = DateTime.Now.D2LSecond();
            return _rfMemberCustomizeShopRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除用户自定义店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfMemberCustomizeShopById(long id)
        {
            return _rfMemberCustomizeShopRepository.DeleteAsync(id);
        }

        #endregion

    }
}

