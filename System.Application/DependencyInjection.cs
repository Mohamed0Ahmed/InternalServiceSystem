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

            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IHelpRequestService, HelpRequestService>();
            services.AddScoped<ICustomerPointsService, CustomerPointsService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAdminService, AdminService>();


            return services;
        }
    }
}