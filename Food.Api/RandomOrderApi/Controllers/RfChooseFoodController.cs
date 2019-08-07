using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Core;
using RandomOrderCore.Common;
using RandomOrderCore.Domains.View;
using RandomOrderCore.Models;
using RandomOrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandomOrderApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RfChooseFoodController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfChooseFoodServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfChooseFoodController(RfChooseFoodServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 选择食物

        /// <summary>
        /// 获取全部选择食物
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfChooseFoodEntity>>))]
        public async Task<IActionResult> GetRfChooseFoodPageList([FromQuery]RfChooseFoodView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(new List<RfChooseFoodEntity>(), totalCount);
            }
            var r = await _cache.GetOrCreate($"RfChooseFoodController_GetRfChooseFoodPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfChooseFoodPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r.Select(t=> new { t.FoodName, t.FoodId , CreateTime = Utils.L2DSecond(t.CreateTime).ToString("yyyy/MM/dd HH:mm:ss") }), totalCount);
        }

        /// <summary>
        /// 根据ID获取选择食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfChooseFoodEntity>))]
        public async Task<IActionResult> GetRfChooseFood(int id)
        {
            return OkResult(await _server.GetRfChooseFood(id));
        }

        /// <summary>
        /// 更新选择食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfChooseFood([FromBody]RfChooseFoodEntity model)
        {
            return OkResult(await _server.ModifyRfChooseFood(model));
        }

        /// <summary>
        /// 插入选择食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfChooseFood([FromBody]RfChooseFoodEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(0);
            }
            return OkResult(await _server.AddRfChooseFood(model));
        }

        /// <summary>
        /// 删除选择食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfChooseFoodById(int id)
        {
            return OkResult(await _server.DelRfChooseFoodById(id));
        }
        #endregion

    }
}

