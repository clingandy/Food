using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace RandomOrderServices
{
    public class RfChooseFoodServer
    {
        #region 构造

        IRfChooseFoodRepository _rfChooseFoodRepository;

        public RfChooseFoodServer(IRfChooseFoodRepository rfChooseFoodRepository)
        {
            _rfChooseFoodRepository = rfChooseFoodRepository;
        }

        #endregion

        #region 选择食物

        /// <summary>
        /// 查询选择食物店信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfChooseFoodEntity>> GetRfChooseFoodPageList(RfChooseFoodView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfChooseFoodRepository.FindRfChooseFoodPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取选择食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfChooseFoodEntity> GetRfChooseFood(long id)
        {
            return _rfChooseFoodRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加选择食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfChooseFood(RfChooseFoodEntity model)
        {
            model.CreateTime = DateTime.Now.D2LSecond();
            return _rfChooseFoodRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改选择食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfChooseFood(RfChooseFoodEntity model)
        {
            return _rfChooseFoodRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除选择食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfChooseFoodById(long id)
        {
            return _rfChooseFoodRepository.DeleteAsync(id);
        }

        #endregion
    }
}
