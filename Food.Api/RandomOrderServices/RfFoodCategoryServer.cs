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
    public class RfFoodCategoryServer
    {
        #region 构造

        IRfFoodCategoryRepository _rfFoodCategoryRepository;

        public RfFoodCategoryServer(IRfFoodCategoryRepository rfFoodCategoryRepository)
        {
            _rfFoodCategoryRepository = rfFoodCategoryRepository;
        }

        #endregion

        #region 食物类型

        /// <summary>
        /// 查询餐饮店信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfFoodCategoryEntity>> GetRfFoodCategoryPageList(RfFoodCategoryView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "sort desc,id desc")
        {
            return _rfFoodCategoryRepository.FindRfFoodCategoryPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取食物类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<List<RfFoodCategoryEntity>> GetRfFoodCategoryAll()
        {
            return _rfFoodCategoryRepository.FindByFuncAsync(t => t.Status == 1);
        }

        /// <summary>
        /// 获取食物类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfFoodCategoryEntity> GetRfFoodCategory(long id)
        {
            return _rfFoodCategoryRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加食物类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfFoodCategory(RfFoodCategoryEntity model)
        {
            model.ModifyTime = DateTime.Now.D2LSecond();
            model.Status = (int)StatusEnum.可用;
            return _rfFoodCategoryRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改食物类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfFoodCategory(RfFoodCategoryEntity model)
        {
            return _rfFoodCategoryRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除食物类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfFoodCategoryById(long id)
        {
            return _rfFoodCategoryRepository.DeleteAsync(id);
        }

        #endregion
    }
}
