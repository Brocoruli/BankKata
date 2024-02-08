using System.Text.RegularExpressions;
using BankKata.Infrastructure.Data;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BankKata.Tests;

public class TestContext
{
    public BankKataDbContext BankKataDbContext;

    public TestContext()
    {
        CheckDatabaseIsForUnitTesting();
        var currentDirectory = GetApplicationRoot();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(currentDirectory);
        configurationBuilder.AddJsonFile(Path.Combine(currentDirectory, "ConfigFiles",
            $"appsettings.Testing.json"), false, reloadOnChange: true);
        configurationBuilder.AddEnvironmentVariables();
        var configuration = configurationBuilder.Build();
        var options = new DbContextOptionsBuilder<BankKataDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DBConnection")).Options;
        BankKataDbContext = new BankKataDbContext(options);
    }
    
    private void CheckDatabaseIsForUnitTesting()
    {
        var currentDirectory = GetApplicationRoot();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(currentDirectory);
        configurationBuilder.AddJsonFile(
            Path.Combine(currentDirectory, "ConfigFiles",
                $"appsettings.Testing.json"), false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();
        var connectionString = configuration.GetConnectionString("DBConnection");
        
        if (!connectionString.ToLower().Contains("test"))
        {
            throw new ConnectionAbortedException(
                "This database is not for testing. Please use test database connection");
        }
    }
    
    private string GetApplicationRoot()
    {
        var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
        var appRoot = appPathMatcher.Match(exePath).Value;
        return appRoot + "..\\..\\BankKata";
    }

    public async Task RespawnDb()
    {
        var con = BankKataDbContext.Database.GetDbConnection();

        await con.OpenAsync();

        await RespawnCheckpoint.Checkpoint().Reset(con);
    }
    
    public BankKataDbContext TestDbContext()
    {
        return BankKataDbContext;
    }
}