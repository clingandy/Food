using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RandomOrderApi.Cache;
using RandomOrderServices;
using System.Threading;
using System.Threading.Tasks;

namespace RandomOrderApi.HostedService
{
    public class ApiBackgroundService : BackgroundService
    {
        private RfFoodCategoryServer _rfFoodCategoryServer;
        private ILogger<ApiBackgroundService> _logger;

        public ApiBackgroundService(ILogger<ApiBackgroundService> logger, RfFoodCategoryServer rfFoodCategoryServer)
        {
            _logger = logger;
            _rfFoodCategoryServer = rfFoodCategoryServer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(() =>
            {
                GetFoodCategoryList();
                while (!stoppingToken.IsCancellationRequested)
                {
                    Thread.Sleep(60 * 60 * 1000);    //休眠60分钟
                    GetFoodCategoryList();
                }

            }, stoppingToken);

            return Task.CompletedTask;
        }

        private async void GetFoodCategoryList()
        {
            try
            {
                // 简单直接获取全部处理
                GlobalStaticCache.FoodCategoryList = await _rfFoodCategoryServer.GetRfFoodCategoryAll();
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "定时获取美食分类信息错误");
            }
        }
    }
}
