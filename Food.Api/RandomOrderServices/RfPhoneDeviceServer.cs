using RandomOrderCore.Domains.View;
using RandomOrderCore.IRepositories;
using RandomOrderCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderServices
{
    public class RfPhoneDeviceServer
    {
        #region 构造

        IRfPhoneDeviceRepository _rfPhoneDeviceRepository;

        public RfPhoneDeviceServer(IRfPhoneDeviceRepository rfPhoneDeviceRepository)
        {
            _rfPhoneDeviceRepository = rfPhoneDeviceRepository;
        }

        #endregion

        #region 手机设备信息

        /// <summary>
        /// 查询手机设备信息信息
        /// </summary>
        /// <param name="search"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByStr">默认主键倒序</param>
        /// <returns></returns>
        public Task<List<RfPhoneDeviceEntity>> GetRfPhoneDevicePageList(RfPhoneDeviceView search, ref int totalCount, int pageIndex = 1, int pageSize = 10, string orderByStr = "open_id desc")
        {
            return _rfPhoneDeviceRepository.FindRfPhoneDevicePageList(search, orderByStr, ref totalCount, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取手机设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<RfPhoneDeviceEntity> GetRfPhoneDevice(string id)
        {
            return _rfPhoneDeviceRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// 添加手机设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddRfPhoneDevice(RfPhoneDeviceEntity model)
        {
            var count = await _rfPhoneDeviceRepository.FindCountByFuncAsync(t=> t.OpenId == model.OpenId);
            if (count <= 0)
            {
                return await _rfPhoneDeviceRepository.InsertAsync(model);
            }
            return 0;
        }

        /// <summary>
        /// 修改手机设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> ModifyRfPhoneDevice(RfPhoneDeviceEntity model)
        {
            return _rfPhoneDeviceRepository.UpdateAsync(model);
        }

        /// <summary>
        /// 删除手机设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> DelRfPhoneDeviceById(string id)
        {
            return _rfPhoneDeviceRepository.DeleteAsync(id);
        }

        #endregion

    }
}

