using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Core;
using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using RandomOrderServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RandomOrderApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RfPhoneDeviceController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfPhoneDeviceServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfPhoneDeviceController(RfPhoneDeviceServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 手机设备信息

        /// <summary>
        /// 获取全部手机设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfPhoneDeviceEntity>>))]
        public async Task<IActionResult> GetRfPhoneDevicePageList([FromQuery]RfPhoneDeviceView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfPhoneDeviceController_GetRfPhoneDevicePageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfPhoneDevicePageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取手机设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfPhoneDeviceEntity>))]
        public async Task<IActionResult> GetRfPhoneDevice(string id)
        {
            return OkResult(await _server.GetRfPhoneDevice(id));
        }

        /// <summary>
        /// 更新手机设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfPhoneDevice([FromBody]RfPhoneDeviceEntity model)
        {
            return OkResult(await _server.ModifyRfPhoneDevice(model));
        }

        /// <summary>
        /// 插入手机设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfPhoneDevice([FromBody]RfPhoneDeviceEntity model)
        {
            return OkResult(await _server.AddRfPhoneDevice(model));
        }

        /// <summary>
        /// 删除手机设备信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfPhoneDeviceById(string id)
        {
            return OkResult(await _server.DelRfPhoneDeviceById(id));
        }
        #endregion

    }
}

