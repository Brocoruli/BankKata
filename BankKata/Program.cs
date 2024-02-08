using BankKata;
using Microsoft.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
        {

            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
            config.AddJsonFile(
                Path.Combine(hostingContext.HostingEnvironment.ContentRootPath, "ConfigFiles",
                    $"appsettings.Development.json"), false, reloadOnChange: true);
            config.AddEnvironmentVariables();
        }).UseStartup<Startup>();
}