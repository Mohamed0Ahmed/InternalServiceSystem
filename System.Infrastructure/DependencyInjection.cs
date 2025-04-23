using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Application.Abstraction;
using System.Infrastructure.Persistence;
using System.Infrastructure.UnitOfWorks;

namespace System.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

       

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppContext")));

            return services;
        }
    }
}
