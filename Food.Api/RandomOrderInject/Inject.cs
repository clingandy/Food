using Microsoft.Extensions.DependencyInjection;
using RandomOrderCore.IRepositories;
using RandomOrderRepositories;
using RandomOrderServices;
using RandomOrderServices.WeChatApi;

namespace RandomOrderInject
{
    public static class Inject
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddSingleton<IRfCateringShopRepository, RfCateringShopRepository>();
            services.AddSingleton<IRfChooseCustomizeShopRepository, RfChooseCustomizeShopRepository>();
            services.AddSingleton<IRfChooseShopRepository, RfChooseShopRepository>();
            services.AddSingleton<IRfMemberCustomizeShopRepository, RfMemberCustomizeShopRepository>();
            services.AddSingleton<IRfMemberRepository, RfMemberRepository>();
            services.AddSingleton<IRfOpinionRepository, RfOpinionRepository>();
            services.AddSingleton<IRfPhoneDeviceRepository, RfPhoneDeviceRepository>();
            services.AddSingleton<IRfChooseFoodRepository, RfChooseFoodRepository>();
            services.AddSingleton<IRfFoodCategoryRepository, RfFoodCategoryRepository>();
            services.AddSingleton<IRfFoodRepository, RfFoodRepository>();

            services.AddSingleton<RfCateringShopServer>();
            services.AddSingleton<RfChooseCustomizeShopServer>();
            services.AddSingleton<RfChooseShopServer>();
            services.AddSingleton<RfMemberCustomizeShopServer>();
            services.AddSingleton<RfMemberServer>();
            services.AddSingleton<RfOpinionServer>();
            services.AddSingleton<RfPhoneDeviceServer>();
            services.AddSingleton<RfChooseFoodServer>();
            services.AddSingleton<RfFoodCategoryServer>();
            services.AddSingleton<RfFoodServer>();
            services.AddSingleton<ApiWeChatServer>();

            services.AddHttpClient("WeChatClient");
        }
    }
}
