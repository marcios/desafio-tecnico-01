using LivrosWebApi.Application.Services;
using LivrosWebApi.Core.Contratcs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LivrosWebApi.Application
{
    public static class LivrosWebApiApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection service, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            service.AddScoped<IGeneroService, GeneroService>();
            service.AddScoped<IAutorService, AutorService>();
            service.AddScoped<ILivroService, LivroService>();
            return service;
        }
    }
}
