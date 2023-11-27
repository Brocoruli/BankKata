using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using WireMock.Server;

namespace BankKata.Tests;

public class BankControllerShould
{
    [Fact]
    public async void MakeADepositCorrectly()
    {
        using var client = GetTestHttpClient();
        var server = WireMockServer.Start(7166);
        var response = new HttpResponseMessage(HttpStatusCode.NotImplemented);

        response = await client.PostAsync("Bank/Deposit/amount",
            new StringContent("500"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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