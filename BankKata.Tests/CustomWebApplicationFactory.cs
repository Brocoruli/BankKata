using System.Net.Http.Headers;
using BankKata.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BankKata.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private readonly IServiceCollection _mockedServices;

    public CustomWebApplicationFactory()
    {
        _mockedServices = new ServiceCollection();
    }

    public void AddTestService<T>(T testService) where T : class
    {
        _mockedServices.TryAddScoped(s => testService);
    }

    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
    }

    protected override void ConfigureClient(HttpClient client)
    {
        base.ConfigureClient(client);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        CheckDatabaseIsForUnitTesting();
        builder.ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
            config.AddJsonFile(
                Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, "ConfigFiles",
                    $"appsettings.Testing.json"), false, reloadOnChange: true);
            config.AddEnvironmentVariables();
        });

        builder.ConfigureTestServices(services =>
        {
            foreach (var mockedService in _mockedServices)
            {
                var mockedType = mockedService.ServiceType;
                ServiceCollectionDescriptorExtensions.RemoveAll(services, mockedType);
                services.Add(mockedService);
            }
        });
    }

    private static void CheckDatabaseIsForUnitTesting()
    {
        var configuration = GetConfiguration();
        var connectionString = configuration.GetConnectionString("DBConnection");
        if (!connectionString.ToLower().Contains("test"))
        {
            throw new ConnectionAbortedException(
                "This database is not for testing. Please use test database connection");
        }
    }
    
    private static IConfiguration GetConfiguration()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(currentDirectory);
        configurationBuilder.AddJsonFile(
        Path.Combine(currentDirectory, "ConfigFiles",
            $"appsettings.Testing.json"), false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();
        return configuration;
    }

    public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using (var scope = this.Services.GetService<IServiceScopeFactory>().CreateScope())
        {
            await action(scope.ServiceProvider);
        }
    }

    public async Task ExecuteDbContextAsync(Func<BankKataDbContext, Task> action)
    {
        await ExecuteScopeAsync(sp => action(sp.GetService<BankKataDbContext>()));
    }

    public async Task RespawnDbContext()
    {
        await ExecuteDbContextAsync(async context =>
        {
            var con = context.Database.GetDbConnection();
            await con.OpenAsync();
            await RespawnCheckpoint.Checkpoint().Reset(con);
        });
    }
}