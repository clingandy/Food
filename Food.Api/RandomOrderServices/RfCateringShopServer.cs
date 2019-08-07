using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.Enum;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfCateringShopServer
    {
        #region 构造

        IRfCateringShopRepository _rfCateringShopRepository;

        public RfCateringShopServer(IRfCateringShopRepository rfCateringShopRepository)
        {
            _rfCateringShopRepository = rfCateringShopRepository;
        }

        #endregion

        #region 餐饮店

        /// <summary>
        /// 查询餐饮店信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfCateringShopEntity>> GetRfCateringShopPageList(RfCateringShopView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfCateringShopRepository.FindRfCateringShopPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取餐饮店
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfCateringShopEntity> GetRfCateringShop(ulong id)
        {
            return _rfCateringShopRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfCateringShop(RfCateringShopEntity model)
        {
            model.CreateTime = DateTime.Now.D2LSecond();
            model.Status = (int)StatusEnum.可用;
            return _rfCateringShopRepository.InsertAsync(model);
        }

        /// <summary>
        /// 批量添加餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddBatchRfCateringShop(List<RfCateringShopEntity> list)
        {
            list = list.Where(t => t.Category.Contains("美食")).ToList();

            // 查询是否已经保存
            var ids = await _rfCateringShopRepository.FindExistId(list.Select(t => ulong.Parse(t.IdStr)).ToList());

            list = list.Where(t => !ids.Contains(ulong.Parse(t.IdStr))).ToList();

            if (list.Any())
            {
                foreach (var model in list)
                {
                    model.Id = ulong.Parse(model.IdStr);
                    model.CreateTime = DateTime.Now.D2LSecond();
                    model.Status = (int)StatusEnum.可用;
                }
                return await _rfCateringShopRepository.InsertBatchAsync(list);
            }
            return 0;
        }

        /// <summary>
        /// 修改餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfCateringShop(RfCateringShopEntity model)
        {
            return _rfCateringShopRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除餐饮店
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfCateringShopById(ulong id)
        {
            return _rfCateringShopRepository.DeleteAsync(id);
        }

        #endregion

    }
}

