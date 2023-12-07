using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace BankKata.Tests;

public class BankControllerShould
{
    [Fact]
    public async void MakeADepositCorrectly()
    {
        using var client = GetTestHttpClient();
        var responseDeposit = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var accountRequest = new AccountRequest()
        {
            id = 1,
            amount = 500
        };
        var request = JsonSerializer.Serialize(accountRequest);
        
        responseDeposit = await client.PostAsync("Bank/Deposit/amount",
            new StringContent(request, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, responseDeposit.StatusCode);

        
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var id = 1;
        
        response = await client.GetAsync($"Bank/GetStatement/{id}");
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<int>(jsonResponse);
        var expectedAccount = new Account()
        {
            Balance = 500,
            Movements = { new Movement(500, 500) }
        };
        Assert.Equal(expectedAccount.Balance, result);
    }
    
    [Fact]
    public async void MakeAWithdrawCorrectly()
    {
        using var client = GetTestHttpClient();
        var responseWithdraw = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var accountRequest = new AccountRequest()
        {
            id = 2,
            amount = 500
        };
        var request = JsonSerializer.Serialize(accountRequest);
        
        responseWithdraw = await client.PostAsync("Bank/Withdraw/amount",
            new StringContent(request, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.OK, responseWithdraw.StatusCode);

        
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var id = 2;
        
        response = await client.GetAsync($"Bank/GetStatement/{id}");
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<int>(jsonResponse);
        var expectedAccount = new Account()
        {
            Balance = 0,
            Movements = { new Movement(500, 500), new Movement(-500, 0) }
        };
        Assert.Equal(expectedAccount.Balance, result);
    }
    
    [Fact]
    public async void NotMakeAWithdrawWhenBalanceFallsBelowZero()
    {
        using var client = GetTestHttpClient();
        var responseWithdraw = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var accountRequest = new AccountRequest()
        {
            id = 2,
            amount = 1000
        };
        var request = JsonSerializer.Serialize(accountRequest);
        
        responseWithdraw = await client.PostAsync("Bank/Withdraw/amount",
            new StringContent(request, Encoding.UTF8, "application/json"));

        Assert.Equal(HttpStatusCode.BadRequest, responseWithdraw.StatusCode);

        
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
        var id = 2;
        
        response = await client.GetAsync($"Bank/GetStatement/{id}");
        
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<int>(jsonResponse);
        var expectedAccount = new Account()
        {
            Balance = 500,
            Movements = { new Movement(500, 500) }
        };
        Assert.Equal(expectedAccount.Balance, result);
    }

    private HttpClient GetTestHttpClient()
    {
        var application = new WebApplicationFactory<Program>();
        var client = application.WithWebHostBuilder(
            builder => builder.ConfigureTestServices(
                services => { }
            )).CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });
        return client;
    }
}