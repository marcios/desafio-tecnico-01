using Asp.Versioning;
using Asp.Versioning.Conventions;
using LivrosWebApi.CrossCutting;

namespace LivrosWebApi
{
    public static class LivrosWebApiModule
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("cors", builder =>
                 builder.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin());
            });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1.0);
                options.ReportApiVersions = true;
                //options.ApiVersionReader = ApiVersionReader.Combine(
                //    new QueryStringApiVersionReader("api-version"),
                //    new MediaTypeApiVersionReader("api-version"),
                //    new HeaderApiVersionReader("x-api-version")
                //    );
            })
        .AddMvc(options =>
        {
            options.Conventions.Add(new VersionByNamespaceConvention());
        })

        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });



            services.AddCrossCuttingModule(configuration);
            return services;
        }
    }
}
