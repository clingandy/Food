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
    public class RfCateringShopController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfCateringShopServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfCateringShopController(RfCateringShopServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 餐饮店

        /// <summary>
        /// 获取全部餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfCateringShopEntity>>))]
        public async Task<IActionResult> GetRfCateringShopPageList([FromQuery]RfCateringShopView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfCateringShopController_GetRfCateringShopPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfCateringShopPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取餐饮店
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfCateringShopEntity>))]
        public async Task<IActionResult> GetRfCateringShop(ulong id)
        {
            return OkResult(await _server.GetRfCateringShop(id));
        }

        /// <summary>
        /// 更新餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfCateringShop([FromBody]RfCateringShopEntity model)
        {
            return OkResult(await _server.ModifyRfCateringShop(model));
        }

        /// <summary>
        /// 插入餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfCateringShop([FromBody]RfCateringShopEntity model)
        {
            return OkResult(await _server.AddRfCateringShop(model));
        }

        /// <summary>
        /// 批量插入餐饮店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddBatchRfCateringShop([FromBody]List<RfCateringShopEntity> list)
        {
            return OkResult(await _server.AddBatchRfCateringShop(list));
        }

        /// <summary>
        /// 删除餐饮店
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfCateringShopById(ulong id)
        {
            return OkResult(await _server.DelRfCateringShopById(id));
        }
        #endregion

    }
}

