using System.Data.Common;
using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;
using Respawn.Graph;

namespace BankKata.Tests.Integration;

public class AccountServicesShould
{
    private BankKataDbContext _context { get; set; }
    private readonly AccountServices _accountServices;
    private readonly AccountRepository _accountRepository;

    public AccountServicesShould()
    {
        string currentDirectory = GetApplicationRoot();
        var configurationBuilder = new ConfigurationBuilder();
        configurationBuilder.SetBasePath(currentDirectory);
        configurationBuilder.AddJsonFile(
            Path.Combine(currentDirectory,
                $"appsettings.Development.json"), false, reloadOnChange: true);
        var configuration = configurationBuilder.Build();
        var options = new DbContextOptionsBuilder<BankKataDbContext>()
            .UseNpgsql(configuration.GetConnectionString("DefaultConnection")).Options;

        _context = new BankKataDbContext(options);
       // _context.Database.EnsureDeletedAsync(); 
        _context.Database.MigrateAsync();
        
        _accountRepository = new AccountRepository(_context);
        _accountServices = new AccountServices(_accountRepository);
        RespawnBankKataDb(_context.Database.GetDbConnection()).Wait();
    }

    public string GetApplicationRoot()
    {
        var exePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
        var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
        var appRoot = appPathMatcher.Match(exePath).Value;
        return appRoot + "..\\..\\BankKata";
    }

    private static async Task RespawnBankKataDb(DbConnection connection)
    {
        await using (var conn = new NpgsqlConnection(connection.ConnectionString))
        {
            conn.OpenAsync();
            var checkpoint = await RespawnCheckpoint.Respawner(conn);
            checkpoint.ResetAsync(conn);
        }
    }

    [Fact]
    public void DepositAmountCorrectlyInTheAccount()
    {
        const int accountId = 1;
        var accountRequest = new AccountRequest()
        {
            Id = 1,
            Amount = 500
        };
        
        _accountServices.Deposit(accountRequest);
        
        var balance = _accountRepository.GetBalance(accountId);
        const int expectedBalance = 500;
        balance.Should().Be(expectedBalance);
    }

}

public static class RespawnCheckpoint
{
    public static async Task<Respawner> Respawner(DbConnection connection)
    {
       var respawner = await Respawn.Respawner.CreateAsync(connection, new RespawnerOptions
        {
            SchemasToInclude = new []
            {
                "public"
            },
            TablesToIgnore = new Table[]
            {
                "__EFMigrationsHistory"
            },
            TablesToInclude = new Table[]
            {
                "Accounts",
                "Movements"
            },
            DbAdapter = DbAdapter.Postgres
        });
        return respawner;
    }
}