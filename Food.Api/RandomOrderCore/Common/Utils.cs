using Newtonsoft.Json;
using RandomOrderCore.Enum;
using RandomOrderCore.Exception;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RandomOrderCore.Common
{
    public static class Utils
    {
        public static void ParamsValidate(bool condition, string errorMsg, int hResult = (int)ResponseCodeEnum.错误)
        {
            if (condition)
            {
                ThrowServiceException(errorMsg, hResult);
            }
        }

        public static void ThrowServiceException(string msg, int hResult = (int)ResponseCodeEnum.错误)
        {
            throw new ServiceDataException(msg, hResult);
        }

        public static string HandleValidate(this bool result, string msg)
        {
            if (result == false)
            {
                throw new System.Exception(msg);
            }

            return msg;
        }

        public static string HandleValidate(this int result, string msg)
        {
            if (result <= 0)
            {
                throw new System.Exception(msg);
            }

            return msg;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static int GetValue(this System.Enum e)
        {
            return e.GetHashCode();
        }

        public static string ToJson(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });

            return json;
        }

        /// <summary>
        /// 返回字段名称和数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetProperties<T>(this T t) where T : class, new()
        {
            var dic = new Dictionary<string, string>();
            if (t == null)
            {
                return dic;
            }
            var properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty);

            if (properties.Length <= 0)
            {
                return dic;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if ((item.PropertyType.IsValueType || item.PropertyType.Name.ToLower() == "string") && !string.IsNullOrEmpty(value?.ToString()))
                {
                    dic.Add(name, value.ToString());
                }
            }
            return dic;
        }

        /// <summary>
        /// 请求API
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="apiUrl"></param>
        /// <param name="reqParameter"></param>
        /// <returns></returns>
        public static async Task<T> ReqApiGet<T>(HttpClient httpClient, string apiUrl, Dictionary<string, string> dicParameter)
        {
            var reqStr = await new FormUrlEncodedContent(dicParameter).ReadAsStringAsync();
            var response = await httpClient.GetAsync($"{apiUrl}?{reqStr}");

            byte[] tmp = await response.Content.ReadAsByteArrayAsync();
            var resultStr = Encoding.UTF8.GetString(tmp);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ThrowServiceException($"HTTP请求错误信息：{resultStr}，请求参数apiUrl：{apiUrl}，reqParameter：{dicParameter.ToJson()}");
            }
            var result = JsonConvert.DeserializeObject<T>(resultStr);
            return result;
        }

        /// <summary>
        /// 请求API
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="apiUrl"></param>
        /// <param name="reqParameter"></param>
        /// <returns></returns>
        public static async Task<T> ReqApiPostJson<T>(HttpClient httpClient, string apiUrl, object data = null)
        {
            var response = await httpClient.PostAsync(apiUrl, new JsonContent(data??""));
            byte[] tmp = await response.Content.ReadAsByteArrayAsync();
            var resultStr = Encoding.UTF8.GetString(tmp);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                ThrowServiceException($"HTTP请求错误信息：{resultStr}，请求参数apiUrl：{apiUrl}，reqParameter：{data?.ToJson()}");
            }
            var result = JsonConvert.DeserializeObject<T>(resultStr);
            return result;
        }

        

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式 秒
        /// </summary>  
        /// <param name="time">时间</param>
        /// <returns>long</returns>  
        public static int D2LSecond(this DateTime time)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            var t = (time.Ticks - startTime.Ticks) / 10000000;   //除10000调整为10位      
            return (int)t;
        }

        /// <summary>
        /// 时间戳转换为日期（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static DateTime L2DSecond(long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(timeStamp).AddHours(8);
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj) :
        base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }
}
