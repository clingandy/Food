using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RandomOrderCore.Common;
using RandomOrderCore.Enum;
using RandomOrderCore.Exception;
using System;
using System.Threading.Tasks;

namespace RandomOrderApi.Middleware
{
    public class VerifySignMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VerifySignMiddleware> _logger;
        public VerifySignMiddleware(RequestDelegate next, ILogger<VerifySignMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.ContentType = "application/Json;charset=utf-8";

            try
            {
                //if (_logger.IsEnabled(LogLevel.Debug))
                //{
                //    _logger.LogDebug($"Path：{context.Request.Path.ToString()}\r\n head：{context.Request.Headers.ToJson()}\r\n Query：{context.Request.Query.ToJson()}\r\n Form：{(context.Request.Method.ToUpper() == "POST" ? context.Request.Form.ToJson() : "[]") }");
                //}
                await _next(context);
                if (context.Response.StatusCode != 200)
                {
                    await context.Response.WriteAsync(new { Code = ResponseCodeEnum.错误, Msg = $"异常未知错误，状态码：{context.Response.StatusCode}" }.ToJson());
                }
            }
            catch (ServiceDataException sec)
            {
                // 业务数据异常，直接抛出
                var code = sec.HResult != (int)ResponseCodeEnum.错误 ? sec.HResult : (int)ResponseCodeEnum.错误;
                await context.Response.WriteAsync(new { Code = code, Msg = sec.Message }.ToJson());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "api未处理错误");
                ex.ToExceptionless()
                    .SetHttpContext(context)
                    .Submit();
                await context.Response.WriteAsync(new { Code = ResponseCodeEnum.错误, Msg = "异常未知错误" }.ToJson());
            } 
        }
    }

    public static class VerifySignMiddlewareExtensions
    {
        public static IApplicationBuilder UseVerifySignMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VerifySignMiddleware>();
        }
    }
}
