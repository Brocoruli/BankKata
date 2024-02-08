
using BankKata.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BankKata;

public static class StartupExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();
    }

    public static void UseSwaggerDocumentationUi(this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider, IConfiguration configuration)
    {
        app.UseSwaggerUI(
            options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            }
        );
    }

    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    public static void UseCorsCustom(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseCors(builder =>
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    }
}