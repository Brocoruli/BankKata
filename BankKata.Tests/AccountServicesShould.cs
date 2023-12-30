using FluentAssertions;

namespace BankKata.Tests;

public class AccountServicesShould
{
    private readonly AccountServices _accountServices;
    private readonly AccountRepository _accountRepository = new();

    public AccountServicesShould()
    {
        _accountServices = new AccountServices(_accountRepository);
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