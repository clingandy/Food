using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RandomOrderApi.Core;
using RandomOrderServices.WeChatApi;
using System;
using System.Threading.Tasks;

namespace RandomOrderApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeChatController : BaseController
    {
        #region 构造

        private readonly IMemoryCache _cache;
        private readonly ApiWeChatServer _server;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatController(ApiWeChatServer server, IMemoryCache cache)
        {
            _cache = cache;
            _server = server;
        }

        #endregion

        #region 接口

        /// <summary>
        /// 获取openid、sessionKey
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public async Task<IActionResult> GetJscode2session(string code)
        {
            return OkResult(await _server.GetJscode2session(code));
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public async Task<IActionResult> GetToken()
        {
            return OkResult(await _server.GetToken());
        }

        /// <summary>
        /// 获取wxaqrcode
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiResult<object>))]
        public async Task<IActionResult> GetCreatewxaqrcode(string url = "pages/index/index")
        {
            var r = await _cache.GetOrCreate($"WeChatController_GetCreatewxaqrcode_url".ToLower(), async (entry) =>
            {
                var img = await _server.GetCreatewxaqrcode(url);
                entry.SetAbsoluteExpiration(TimeSpan.FromDays(1));
                return img;
            });
            return OkResult(r);
        }

        #endregion
    }

}
