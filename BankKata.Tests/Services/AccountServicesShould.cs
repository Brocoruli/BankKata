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
    public void DepositAmountCorrectlyInTheAccount()
    {
        const int accountId = 1;
        var accountRequest = new AccountRequest()
        {
            Id = 1,
            Amount = 500
        };
        
        _accountServices.Deposit(accountRequest);
        
        var balance = _accountRepositoryOnMemory.GetBalance(accountId);
        const int expectedBalance = 500;
        balance.Should().Be(expectedBalance);
    }
    
    [Fact]
    public void MakeAWithdrawCorrectly()
    {
        const int accountId = 2;
        var accountRequest = new AccountRequest()
        {
            Id = 2,
            Amount = 500
        };
        
        _accountServices.Withdraw(accountRequest);
        
        var balance = _accountRepositoryOnMemory.GetBalance(accountId);
        const int expectedBalance = 0;
        balance.Should().Be(expectedBalance);
    }
}