using BankKata.Tests.Repositories;
using FluentAssertions;

namespace BankKata.Tests.Services;

public class AccountServicesShould
{
    private readonly AccountServices _accountServices;
    private readonly AccountRepositoryOnMemory _accountRepositoryOnMemory = new();

    public AccountServicesShould()
    {
        _accountServices = new AccountServices(_accountRepositoryOnMemory);
    }

    [Fact]
    public async Task DepositAmountCorrectlyInTheAccount()
    {
        var accountId = new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae");
        var accountRequest = new AccountRequest()
        {
            Id = accountId,
            Amount = 500
        };
        
        _accountServices.Deposit(accountRequest);
        
        var account = await _accountRepositoryOnMemory.Find(accountId);
        const int expectedBalance = 500;
        account.GetBalance().Should().Be(expectedBalance);
    }
    
    [Fact]
    public async Task MakeAWithdrawCorrectly()
    {
        var accountId = new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c");
        var accountRequest = new AccountRequest()
        {
            Id = accountId,
            Amount = 500
        };
        
        _accountServices.Withdraw(accountRequest);
        
        var account = await _accountRepositoryOnMemory.Find(accountId);
        const int expectedBalance = 0;
        account.GetBalance().Should().Be(expectedBalance);
    }
}