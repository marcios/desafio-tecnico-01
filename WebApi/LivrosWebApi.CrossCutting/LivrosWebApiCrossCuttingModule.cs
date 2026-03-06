using LivrosWebApi.Application;
using LivrosWebApi.Core;
using LivrosWebApi.Data;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosWebApi.CrossCutting
{
    public static class LivrosWebApiCrossCuttingModule
    {
        public static IServiceCollection AddCrossCuttingModule(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {

            services.AddDataModule(configuration);

            services.AddCoreModule(configuration);

            services.AddApplicationModule(configuration);


            return services;
        }

      
    }
}
