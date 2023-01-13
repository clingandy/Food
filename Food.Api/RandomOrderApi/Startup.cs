using Exceptionless;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomOrderApi.HostedService;
using RandomOrderApi.Middleware;
using RandomOrderCore.Config;
using RandomOrderInject;
using System.Collections.Generic;

namespace RandomOrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            });
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options => //开启参数验证过滤器
            {
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            // MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // 本地缓存
            services.AddMemoryCache();
            
            // 静态缓存
            services.AddHostedService<ApiBackgroundService>();

            // 注入
            services.InjectServices();
            // MySql数据注入
            services.Configure<List<MySqlConnectionConfig>>(Configuration.GetSection("MySqlConfig"));

            // API文档
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //使用API文档
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            // Exceptionless日志
            ExceptionlessClient.Default.SubmittingEvent += OnSubmittingEvent;
            app.UseExceptionless("KK4oJQ7njo8xlAPmtfMaCbcJXph9Wy11WEzgbAos");
            //// NLog作为日志记录工具
            //loggerFactory.AddNLog();
            //// 引入Nlog配置文件
            //env.ConfigureNLog("nlog.config");

            app.UseVerifySignMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// 全局配置Exceptionless
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSubmittingEvent(object sender, EventSubmittingEventArgs e)
        {
            // 只处理未处理的异常
            //if (!e.IsUnhandledError)
            //    return;

            // 忽略404错误
            if (e.Event.IsNotFound())
            {
                e.Cancel = true;
                return;
            }

            // 忽略没有错误体的错误
            var error = e.Event.GetError();
            if (error == null)
                return;
            // 忽略 401 (Unauthorized) 和 请求验证的错误.
            if (error.Code == "401" || error.Type == "System.Web.HttpRequestValidationException")
            {
                e.Cancel = true;
                return;
            }
            // Ignore any exceptions that were not thrown by our code.
            //var handledNamespaces = new List<string> { "Exceptionless" };
            //if (!error.StackTrace.Select(s => s.DeclaringNamespace).Distinct().Any(ns => handledNamespaces.Any(ns.Contains)))
            //{
            //    e.Cancel = true;
            //    return;
            //}
            // 添加附加信息.
            // e.Event.AddObject(obj, "obj", excludedPropertyNames: new[] { "objField" }, maxDepth: 2);
            // e.Event.Tags.Add("RandomOrderApi");
            // e.Event.MarkAsCritical();
        }
    }
}
