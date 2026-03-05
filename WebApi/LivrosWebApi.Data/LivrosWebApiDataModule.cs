using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Data.Contexts;
using LivrosWebApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosWebApi.Data
{
    public static class LivrosWebApiDataModule
    {
        public static IServiceCollection AddDataModule(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("Default");

            services.AddDbContext<LivrosDbContext>(options =>
                options.UseMySQL(connection!, o=>
                {
                    o.MigrationsAssembly("LivrosWebApi.Data");
                }).LogTo(Console.WriteLine).EnableDetailedErrors()
                
             );


            services.AddScoped<IGeneroRepository, GeneroRepository>();

            return services;


        }
    }
}
