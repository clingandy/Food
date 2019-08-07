using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfOpinionServer
    {
        #region 构造

        IRfOpinionRepository _rfOpinionRepository;

        public RfOpinionServer(IRfOpinionRepository rfOpinionRepository)
        {
            _rfOpinionRepository = rfOpinionRepository;
        }

        #endregion

        #region 意见

        /// <summary>
        /// 查询意见信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfOpinionEntity>> GetRfOpinionPageList(RfOpinionView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfOpinionRepository.FindRfOpinionPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfOpinionEntity> GetRfOpinion(int id)
        {
            return _rfOpinionRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddRfOpinion(RfOpinionEntity model)
        {
            model.CreateTime = DateTime.Now.D2LSecond();
            return _rfOpinionRepository.InsertAsync(model);
        }

        /// <summary>
        /// 修改意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfOpinion(RfOpinionEntity model)
        {
            return _rfOpinionRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfOpinionById(int id)
        {
            return _rfOpinionRepository.DeleteAsync(id);
        }

        #endregion

    }
}

