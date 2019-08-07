using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Cache;
using RandomOrderApi.Core;
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
    public class RfFoodCategoryController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfFoodCategoryServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfFoodCategoryController(RfFoodCategoryServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 食物分类

        /// <summary>
        /// 获取全部食物分类
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfFoodCategoryEntity>>))]
        public async Task<IActionResult> GetRfFoodCategoryPageList([FromQuery]RfFoodCategoryView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfFoodCategoryController_GetRfFoodCategoryPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                var list = await _server.GetRfFoodCategoryPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 根据ID获取食物分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfFoodCategoryEntity>>))]
        public IActionResult GetRfFoodCategoryAll()
        {
            var r = GlobalStaticCache.FoodCategoryList;
            return OkResult(r.Select(t => new { t.Id, t.Name, t.PId }));
        }

        /// <summary>
        /// 根据ID获取食物分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfFoodCategoryEntity>))]
        public async Task<IActionResult> GetRfFoodCategory(int id)
        {
            return OkResult(await _server.GetRfFoodCategory(id));
        }

        /// <summary>
        /// 更新食物分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfFoodCategory([FromBody]RfFoodCategoryEntity model)
        {
            return OkResult(await _server.ModifyRfFoodCategory(model));
        }

        /// <summary>
        /// 插入食物分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfFoodCategory([FromBody]RfFoodCategoryEntity model)
        {
            return OkResult(await _server.AddRfFoodCategory(model));
        }

        /// <summary>
        /// 删除食物分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfFoodCategoryById(int id)
        {
            return OkResult(await _server.DelRfFoodCategoryById(id));
        }
        #endregion

    }
}

