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
    public class RfMemberCustomizeShopController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfMemberCustomizeShopServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfMemberCustomizeShopController(RfMemberCustomizeShopServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 用户自定义店铺

        /// <summary>
        /// 获取全部用户自定义店铺
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfMemberCustomizeShopEntity>>))]
        public async Task<IActionResult> GetRfMemberCustomizeShopPageList([FromQuery]RfMemberCustomizeShopView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            if (model.OpenId.IsNullOrWhiteSpace())
            {
                return OkResult(new List<RfMemberCustomizeShopEntity>(), totalCount);
            }
            var r = await _cache.GetOrCreate($"RfMemberCustomizeShopController_GetRfMemberCustomizeShopPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfMemberCustomizeShopPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取用户自定义店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfMemberCustomizeShopEntity>))]
        public async Task<IActionResult> GetRfMemberCustomizeShop(long id)
        {
            return OkResult(await _server.GetRfMemberCustomizeShop(id));
        }

        /// <summary>
        /// 更新用户自定义店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfMemberCustomizeShop([FromBody]RfMemberCustomizeShopEntity model)
        {
            return OkResult(await _server.ModifyRfMemberCustomizeShop(model));
        }

        /// <summary>
        /// 插入用户自定义店铺
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfMemberCustomizeShop([FromBody]RfMemberCustomizeShopEntity model)
        {
            if (model.OpenId.IsNullOrWhiteSpace() || model.Title.IsNullOrWhiteSpace() || model.Category.IsNullOrWhiteSpace())
            {
                return OkResult(0);
            }
            return OkResult(await _server.AddRfMemberCustomizeShop(model));
        }

        /// <summary>
        /// 删除用户自定义店铺
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfMemberCustomizeShopById(long id)
        {
            return OkResult(await _server.DelRfMemberCustomizeShopById(id));
        }
        #endregion

    }
}

