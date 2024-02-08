using System.Net;
using System.Text;
using BankKata.Domain.Entities;
using BankKata.Tests.BogusData;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BankKata.Tests;

public class BankControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
{
    private readonly CustomWebApplicationFactory<Startup> _factory;
    private readonly HttpClient _httpClient;

    public BankControllerShould(CustomWebApplicationFactory<Startup> factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient();
        Task.WaitAll(_factory.RespawnDbContext());
    }

    [Fact]
    public async void MakeADepositCorrectly()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            await context.Database.ExecuteSqlRawAsync(AccountSeedData.AccountWithMovements());
            await context.SaveChangesAsync();
        });
        
        var responseDeposit = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var id = new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae");
        var accountRequest = new AccountRequest()
        {
            Id = id,
            Amount = 500
        };
        var request = JsonConvert.SerializeObject(accountRequest);
        
        responseDeposit = await _httpClient.PostAsync("api/Bank/Deposit",
            new StringContent(request, Encoding.UTF8, "application/json"));
        
        Assert.Equal(HttpStatusCode.OK, responseDeposit.StatusCode);
        
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        
        response = await _httpClient.GetAsync($"api/Bank/GetAccount/{id}");
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Account>(jsonResponse);
        var expectedAccount = new Account()
        {
            Id = id,
            Balance = 500,
            Movements = { new Movement(500, 500) }
        };
        Assert.Equal(expectedAccount.Balance, result.GetBalance());
    }
    
    [Fact]
    public async void MakeAWithdrawCorrectly()
    {
        await _factory.ExecuteDbContextAsync(async context =>
        {
            await context.Database.ExecuteSqlRawAsync(AccountSeedData.AccountWithMovements());
            await context.SaveChangesAsync();
        });
        
        var responseWithdraw = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var id = new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c");
        var accountRequest = new AccountRequest()
        {
            Id = id,
            Amount = 500
        };
        var request = JsonConvert.SerializeObject(accountRequest);
        
        responseWithdraw = await _httpClient.PostAsync("api/Bank/Withdraw",
            new StringContent(request, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, responseWithdraw.StatusCode);
        
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        
        response = await _httpClient.GetAsync($"api/Bank/GetAccount/{id}");
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<Account>(jsonResponse);
        var expectedAccount = new Account()
        {
            Id = id,
            Balance = 0,
            Movements = { new Movement(500, 500), new Movement(-500, 0) }
        };
        Assert.Equal(expectedAccount.Balance, result.GetBalance());
    }
    
    // [Fact]
    // public async void NotMakeAWithdrawWhenBalanceFallsBelowZero()
    // {
    //     using var client = GetTestHttpClient();
    //     var responseWithdraw = new HttpResponseMessage(HttpStatusCode.NotImplemented);
    //     var accountRequest = new AccountRequest()
    //     {
    //         Id = 2,
    //         Amount = 1000
    //     };
    //     var request = JsonSerializer.Serialize(accountRequest);
    //     
    //     responseWithdraw = await client.PostAsync("Bank/Withdraw/amount",
    //         new StringContent(request, Encoding.UTF8, "application/json"));
    //
    //     Assert.Equal(HttpStatusCode.BadRequest, responseWithdraw.StatusCode);
    //
    //     
    //     var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
    //     var id = 2;
    //     
    //     response = await client.GetAsync($"Bank/GetStatement/{id}");
    //     
    //     var jsonResponse = await response.Content.ReadAsStringAsync();
    //     var result = JsonSerializer.Deserialize<int>(jsonResponse);
    //     var expectedAccount = new Account()
    //     {
    //         Balance = 500,
    //         Movements = { new Movement(500, 500) }
    //     };
    //     Assert.Equal(expectedAccount.Balance, result);
    // }
}