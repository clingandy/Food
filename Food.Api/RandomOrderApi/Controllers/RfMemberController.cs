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
    public class RfMemberController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfMemberServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfMemberController(RfMemberServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 微信用户信息表

        /// <summary>
        /// 获取全部微信用户信息表
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfMemberEntity>>))]
        public async Task<IActionResult> GetRfMemberPageList([FromQuery]RfMemberView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfMemberController_GetRfMemberPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfMemberPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取微信用户信息表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfMemberEntity>))]
        public async Task<IActionResult> GetRfMember(int id)
        {
            return OkResult(await _server.GetRfMember(id));
        }

        /// <summary>
        /// 更新微信用户信息表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfMember([FromBody]RfMemberEntity model)
        {
            return OkResult(await _server.ModifyRfMember(model));
        }

        /// <summary>
        /// 插入微信用户信息表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfMember([FromBody]RfMemberEntity model)
        {
            return OkResult(await _server.AddRfMember(model));
        }

        /// <summary>
        /// 删除微信用户信息表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfMemberById(int id)
        {
            return OkResult(await _server.DelRfMemberById(id));
        }
        #endregion

    }
}

