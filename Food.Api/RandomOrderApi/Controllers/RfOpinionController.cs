using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Core;
using RandomOrderCore.Common;
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
    public class RfOpinionController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfOpinionServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfOpinionController(RfOpinionServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 意见

        /// <summary>
        /// 获取全部意见
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfOpinionEntity>>))]
        public async Task<IActionResult> GetRfOpinionPageList([FromQuery]RfOpinionView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfOpinionController_GetRfOpinionPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfOpinionPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfOpinionEntity>))]
        public async Task<IActionResult> GetRfOpinion(int id)
        {
            return OkResult(await _server.GetRfOpinion(id));
        }

        /// <summary>
        /// 更新意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfOpinion([FromBody]RfOpinionEntity model)
        {
            return OkResult(await _server.ModifyRfOpinion(model));
        }

        /// <summary>
        /// 插入意见
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfOpinion([FromBody]RfOpinionEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace() || model.Opinion.IsNullOrWhiteSpace() || model.ContactInfo.IsNullOrWhiteSpace())
            {
                return OkResult(0);
            }
            return OkResult(await _server.AddRfOpinion(model));
        }

        /// <summary>
        /// 删除意见
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfOpinionById(int id)
        {
            return OkResult(await _server.DelRfOpinionById(id));
        }
        #endregion

    }
}

