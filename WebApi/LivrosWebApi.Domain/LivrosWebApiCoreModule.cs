using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosWebApi.Core
{
    public static class LivrosWebApiCoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
