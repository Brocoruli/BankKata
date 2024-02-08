using BankKata.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

namespace BankKata;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors();
        services.AddRepositories();
        services.AddVersioning();
        services.AddScoped<AccountServices>();
        services.AddControllers();
        services.AddSwagger();
        services.AddDbContext<BankKataDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DBConnection")));
        

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCorsCustom(Configuration);
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseSwagger();
        app.UseSwaggerDocumentationUi(provider, Configuration);
    }
}