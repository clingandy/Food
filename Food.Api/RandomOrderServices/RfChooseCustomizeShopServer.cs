using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfChooseCustomizeShopServer
    {
        #region 构造

        IRfChooseCustomizeShopRepository _rfChooseCustomizeShopRepository;

        public RfChooseCustomizeShopServer(IRfChooseCustomizeShopRepository rfChooseCustomizeShopRepository)
        {
            _rfChooseCustomizeShopRepository = rfChooseCustomizeShopRepository;
        }

        #endregion

        #region 随机选择的店铺

        /// <summary>
        /// 查询随机选择的店铺信息
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfChooseCustomizeShopEntity>> GetRfChooseCustomizeShopPageList(string openId, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfChooseCustomizeShopRepository.FindRfChooseCustomizeShopPageList(openId, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfChooseCustomizeShopEntity> GetRfChooseCustomizeShop(long id)
        {
            return _rfChooseCustomizeShopRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfChooseCustomizeShop(RfChooseCustomizeShopEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return Task.FromResult(0);
            }
            model.CreateTime = DateTime.Now.D2LSecond();
            return _rfChooseCustomizeShopRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfChooseCustomizeShop(RfChooseCustomizeShopEntity model)
        {
            return _rfChooseCustomizeShopRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfChooseCustomizeShopById(long id)
        {
            return _rfChooseCustomizeShopRepository.DeleteAsync(id);
        }

        #endregion

    }
}

