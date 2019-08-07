using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Cache;
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
    public class RfFoodController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly RfFoodServer _server;
        private readonly RfChooseFoodServer _serverChooseFood;

        /// <summary>
        /// 构造方法
        /// </summary>
        public RfFoodController(RfFoodServer server, RfChooseFoodServer serverChooseFood, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
            _serverChooseFood = serverChooseFood;
        }

        #endregion

        #region 食物

        /// <summary>
        /// 获取全部食物
        /// </summary>
        /// <param name="model"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<List<RfFoodEntity>>))]
        public async Task<IActionResult> GetRfFoodPageList([FromQuery]RfFoodView model, int pageIndex = 1, int pageSize = 10)
        {
            var totalCount = 0;
            var r = await _cache.GetOrCreate($"RfFoodController_GetRfFoodPageList_{model.GetHashCode()}_{pageIndex}_{pageSize})".ToLower(), async (entry) =>
            {
                if (model.Category != null && model.Category > 0)
                {
                    var categoryList = GlobalStaticCache.FoodCategoryList;
                    model.CategoryList = categoryList.Where(t => t.Id == model.Category).Select(t => t.Id).ToList();
                    var categoryIds = categoryList.Where(t=> t.PId == model.Category).Select(t=> t.Id).ToList();
                    if (categoryIds.Any())
                    {
                        model.CategoryList.AddRange(categoryIds);
                    }
                    foreach (var categoryId in categoryIds)
                    {
                        var categoryCIds = categoryList.Where(t => t.PId == categoryId).Select(t => t.Id).ToList();
                        if(categoryCIds.Any())
                        {
                            model.CategoryList.AddRange(categoryCIds);
                        }
                    }
                }

                var list = await _server.GetRfFoodPageList(model, ref totalCount, pageIndex, pageSize);
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1));
                return list;
            });
            return OkResult(r, totalCount);
        }

        /// <summary>
        /// 按条件随机返回第一个食物ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> GetRfFoodByRand([FromQuery]RfFoodView query)
        {
            var id = await _server.GetRfFoodByRand(query);
            if (id > 0 && !query.OpenId.IsNullOrWhiteSpace())
            {
                var model = await _server.GetRfFood(id) ?? new RfFoodEntity();
                await _serverChooseFood.AddRfChooseFood(new RfChooseFoodEntity {
                    FoodId = id,
                    FoodName = model.Name,
                    OpenId = query.OpenId
                });
            }
            return OkResult(id);
        }

        /// <summary>
        /// 根据ID获取食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<RfFoodEntity>))]
        public async Task<IActionResult> GetRfFood(int id)
        {
            var model = await _server.GetRfFood(id);
            var details = model.Details;
            //var Details = System.Text.RegularExpressions.Regex.Replace(details, @"</?[^>]*>", "");
            //details = System.Text.RegularExpressions.Regex.Replace(details, @"<\s*img[^>]*>(.*?)<\s*/\s*img>|<\s*img[^>]*>|<pr />", "");
            return OkResult( new { details, model.Name, model.ImgUrl });
        }

        /// <summary>
        /// 更新食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfFood([FromBody]RfFoodEntity model)
        {
            return OkResult(await _server.ModifyRfFood(model));
        }

        /// <summary>
        /// 更新食物Sort
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> ModifyRfFoodSort(int id, int sort)
        {
            if (id <=0 || sort <= 0)
            {
                return OkResult(0);
            }
            return OkResult(await _server.ModifyRfFoodSort(id, sort));
        }

        /// <summary>
        /// 插入食物
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> AddRfFood([FromBody]RfFoodEntity model)
        {
            return OkResult(await _server.AddRfFood(model));
        }

        /// <summary>
        /// 删除食物
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200, Type = typeof(ApiResult<int>))]
        public async Task<IActionResult> DelRfFoodById(int id)
        {
            return OkResult(await _server.DelRfFoodById(id));
        }
        #endregion

    }
}

