using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Application.Interfaces;
using System.Application.Services;

namespace System.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IHelpRequestService, HelpRequestService>();
            services.AddScoped<IPointsSettingService, PointsSettingService>();
            services.AddScoped<IGuestPointsService, GuestPointsService>();

            return services;
        }
    }
}