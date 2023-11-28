using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using WireMock.Server;

namespace BankKata.Tests;

public class BankControllerShould
{
    [Fact]
    public async void MakeADepositCorrectly()
    {
        using var client = GetTestHttpClient();
        var responseDeposit = new HttpResponseMessage(HttpStatusCode.NotImplemented);
 
        responseDeposit = await client.PostAsync("Bank/Deposit/amount",
            new StringContent("500"));

        Assert.Equal(HttpStatusCode.OK, responseDeposit.StatusCode);

        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);

        response = await client.GetAsync("Bank/GetStatement");
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Account>(jsonResponse);
        var expectedAccount = new Account()
        {
            Balance = 500,
            Movements = { new Movement(500, 500) }
        };
        Assert.Equal(expectedAccount.Balance, result.Balance);
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