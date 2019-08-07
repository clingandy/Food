using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RandomOrderCore.Enum;

namespace RandomOrderApi.Core
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 业务处理成功
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        protected IActionResult OkResult<T>(T obj, long totalCount = 0)
        {
            return new JsonResult(new ApiResult<T> { Code = ResponseCodeEnum.成功, Data = obj, Msg = "", TotalCount = totalCount }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        /// <summary>
        /// 业务处理失败
        /// </summary>
        /// <param name="msg"></param>
        ///  <param name="obj"></param>
        /// <returns></returns>
        protected IActionResult BadResult<T>(string msg, T obj = default)
        {
            return new JsonResult(new ApiResult<T> { Code = ResponseCodeEnum.失败, Data = obj, Msg = msg }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        public class ApiResult<T>
        {
            public ResponseCodeEnum Code { get; set; }

            public T Data { get; set; }

            public string Msg { get; set; }

            public long TotalCount { get; set; }
        }
    }
}
