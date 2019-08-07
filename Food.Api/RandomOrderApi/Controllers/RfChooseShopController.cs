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
    public class RfChooseShopController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfChooseShopServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfChooseShopController(RfChooseShopServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 随机选择的店铺

        /// <summary>
        /// 获取全部随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfChooseShopEntity>>))]
        public async Task<IActionResult> GetRfChooseShopPageList([FromQuery]RfChooseShopView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(new List<RfChooseShopEntity>(), totalCount);
            }
            var r = await _cache.GetOrCreate($"RfChooseShopController_GetRfChooseShopPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfChooseShopPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(5));
                return list;
            });
            var rr = r.Select(t => new { Category = t.Category.Replace("美食:", ""), t.CateringShopTitle, CreateTime = Utils.L2DSecond(t.CreateTime).ToString("yyyy/MM/dd HH:mm:ss") });
            return OkResult(rr, totalCount);
        }

        /// <summary>
        /// 根据ID获取随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfChooseShopEntity>))]
        public async Task<IActionResult> GetRfChooseShop(ulong id)
        {
            return OkResult(await _server.GetRfChooseShop(id));
        }

        /// <summary>
        /// 更新随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfChooseShop([FromBody]RfChooseShopEntity model)
        {
            return OkResult(await _server.ModifyRfChooseShop(model));
        }

        /// <summary>
        /// 插入随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfChooseShop([FromBody]RfChooseShopEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(0);
            }
            return OkResult(await _server.AddRfChooseShop(model));
        }

        /// <summary>
        /// 删除随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfChooseShopById(ulong id)
        {
            return OkResult(await _server.DelRfChooseShopById(id));
        }
        #endregion

    }
}

