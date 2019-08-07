using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfMemberServer
    {
        #region 构造

        IRfMemberRepository _rfMemberRepository;

        public RfMemberServer(IRfMemberRepository rfMemberRepository)
        {
            _rfMemberRepository = rfMemberRepository;
        }

        #endregion

        #region 微信用户信息表

        /// <summary>
        /// 查询微信用户信息表信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfMemberEntity>> GetRfMemberPageList(RfMemberView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "id desc")
        {
            return _rfMemberRepository.FindRfMemberPageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取微信用户信息表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfMemberEntity> GetRfMember(int id)
        {
            return _rfMemberRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加微信用户信息表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddRfMember(RfMemberEntity model)
        {
            var queryModel = await _rfMemberRepository.FindFirstByFuncAsync(t=> t.OpenId == model.OpenId);
            if (queryModel != null)
            {
                queryModel.LoginCount++;
                queryModel.AvatarUrl = model.AvatarUrl;
                queryModel.NickName = model.NickName;
                queryModel.ModifyTime = DateTime.Now.D2LSecond();
                return await _rfMemberRepository.UpdateAsync(queryModel);
            }
            else
            {
                model.LoginCount = 1;
                model.CreateTime = DateTime.Now.D2LSecond();
                return await _rfMemberRepository.InsertAsync(model);
            }
        }

        /// <summary>
        /// 修改微信用户信息表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfMember(RfMemberEntity model)
        {
            return _rfMemberRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除微信用户信息表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfMemberById(int id)
        {
            return _rfMemberRepository.DeleteAsync(id);
        }

        #endregion

    }
}

