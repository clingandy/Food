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
    public class RfChooseCustomizeShopController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfChooseCustomizeShopServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfChooseCustomizeShopController(RfChooseCustomizeShopServer server, IMemoryCache cache)
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
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfChooseCustomizeShopEntity>>))]
        public async Task<IActionResult> GetRfChooseCustomizeShopPageList(string openId, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            if (openId.IsNullOrWhiteSpace())
            {
                return OkResult(new List<RfChooseCustomizeShopEntity>(), totalCount);
            }
            var r = await _cache.GetOrCreate($"RfChooseCustomizeShopController_GetRfChooseCustomizeShopPageList_{openId}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfChooseCustomizeShopPageList(openId, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(5));
                return list;
            });
            var rr = r.Select(t => new { t.Category, t.ShopTitle, CreateTime = Utils.L2DSecond(t.CreateTime).ToString("yyyy/MM/dd HH:mm:ss") });
            return OkResult(rr, totalCount);
        }

        /// <summary>
        /// 根据ID获取随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfChooseCustomizeShopEntity>))]
        public async Task<IActionResult> GetRfChooseCustomizeShop(long id)
        {
            return OkResult(await _server.GetRfChooseCustomizeShop(id));
        }

        /// <summary>
        /// 更新随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfChooseCustomizeShop([FromBody]RfChooseCustomizeShopEntity model)
        {
            return OkResult(await _server.ModifyRfChooseCustomizeShop(model));
        }

        /// <summary>
        /// 插入随机选择的店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfChooseCustomizeShop([FromBody]RfChooseCustomizeShopEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(0);
            }
            return OkResult(await _server.AddRfChooseCustomizeShop(model));
        }

        /// <summary>
        /// 删除随机选择的店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfChooseCustomizeShopById(long id)
        {
            return OkResult(await _server.DelRfChooseCustomizeShopById(id));
        }
        #endregion

    }
}

