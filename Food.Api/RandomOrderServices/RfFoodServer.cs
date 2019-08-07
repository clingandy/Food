using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.Enum;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfFoodServer
    {
        #region 构造

        IRfFoodRepository _rfFoodRepository;

        public RfFoodServer(IRfFoodRepository rfFoodRepository)
        {
            _rfFoodRepository = rfFoodRepository;
        }

        #endregion

        #region 食物

        /// <summary>
        /// 查询食物信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfFoodEntity>> GetRfFoodPageList(RfFoodView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id")
        {
            return _rfFoodRepository.FindRfFoodPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 按条件随机返回第一个食物ID
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public Task<int> GetRfFoodByRand(RfFoodView search)
        {
            return _rfFoodRepository.FindRfFoodByRand(search);
        }

        /// <summary>
        /// 获取食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfFoodEntity> GetRfFood(int id)
        {
            return _rfFoodRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfFood(RfFoodEntity model)
        {
            model.ModifyTime = DateTime.Now.D2LSecond();
            model.Status = (int)StatusEnum.可用;
            return _rfFoodRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfFood(RfFoodEntity model)
        {
            model.ModifyTime = DateTime.Now.D2LSecond();
            return _rfFoodRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 修改食物Sort
        /// </summary>
        /// <param name="id"></param>
        ///  <param name="sort"></param>
        /// <returns></returns>
        public Task<int> ModifyRfFoodSort(int id, int sort)
        {
            var sortTemp = sort - 1;
            return _rfFoodRepository.UpdateAsync(new { sort }, t=> t.Id == id && t.Sort == sortTemp);
        }

        /// <summary>
        /// 删除食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfFoodById(long id)
        {
            return _rfFoodRepository.DeleteAsync(id);
        }

        #endregion
    }
}
