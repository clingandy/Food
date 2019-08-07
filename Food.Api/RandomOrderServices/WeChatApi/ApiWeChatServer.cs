using RandomOrderCore.Common;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RandomOrderServices.WeChatApi
{
    public class ApiWeChatServer
    {
        #region 构造

        private IHttpClientFactory _httpClientFactory;

        public ApiWeChatServer(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        #endregion

        /// <summary>
        /// 获取openid、sessionKey
        /// </summary>
        /// <returns></returns>
        public Task<object> GetJscode2session(string code)
        {
            var dicParameter = new Dictionary<string, string>
            {
                { "appid", ApiConfig.appid },
                { "secret", ApiConfig.secret },
                { "js_code", code },
                { "grant_type", "authorization_code" }
            };
            return Utils.ReqApiGet<object>(GetHttpClient(), ApiConfig.jscode2sessionPath, dicParameter);
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public Task<object> GetToken()
        {
            var dicParameter = new Dictionary<string, string>
            {
                { "appid", ApiConfig.appid },
                { "secret", ApiConfig.secret },
                { "grant_type", "client_credential" }
            };
            return Utils.ReqApiGet<object>(GetHttpClient(), ApiConfig.tokenPath, dicParameter);
        }

        /// <summary>
        /// 获取wxaqrcode
        /// </summary>
        /// <returns></returns>
        public async Task<object> GetCreatewxaqrcode(string url)
        {
            dynamic obj = GetToken().Result;
            object parameter = new
            {
                path = url,
                width = 280
            };
            var response = await GetHttpClient().PostAsync($"{ApiConfig.createwxaqrcodePath}?access_token={obj.access_token}", new JsonContent(parameter ?? ""));
            byte[] array = await response.Content.ReadAsByteArrayAsync();
            return Convert.ToBase64String(array);

        }

        /// <summary>
        /// 通用HttpClient
        /// </summary>
        /// <returns></returns>
        private HttpClient GetHttpClient()
        {
            var weChatCilent = _httpClientFactory.CreateClient("WeChatClient");
            weChatCilent.BaseAddress = new Uri("https://api.weixin.qq.com");
            return weChatCilent;
        }
    }
}
