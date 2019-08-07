using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfChooseShopServer
    {
        #region 构造

        IRfChooseShopRepository _rfChooseShopRepository;

        public RfChooseShopServer(IRfChooseShopRepository rfChooseShopRepository)
        {
            _rfChooseShopRepository = rfChooseShopRepository;
        }

        #endregion

        #region 随机选择的店铺

        /// <summary>
        /// 查询随机选择的店铺信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfChooseShopEntity>> GetRfChooseShopPageList(RfChooseShopView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfChooseShopRepository.FindRfChooseShopPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfChooseShopEntity> GetRfChooseShop(ulong id)
        {
            return _rfChooseShopRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfChooseShop(RfChooseShopEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace() || !model.Category.Contains("美食"))
            {
                return Task.FromResult(0);
            }
            model.CreateTime = DateTime.Now.D2LSecond();
            model.CateringShopId = ulong.Parse(model.CateringShopIdStr);
            return _rfChooseShopRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfChooseShop(RfChooseShopEntity model)
        {
            return _rfChooseShopRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfChooseShopById(ulong id)
        {
            return _rfChooseShopRepository.DeleteAsync(id);
        }

        #endregion

    }
}

