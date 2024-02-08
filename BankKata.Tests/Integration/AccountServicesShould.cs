using BankKata.Domain.Entities;
using BankKata.Infrastructure.Data.Repositories;
using FluentAssertions;

namespace BankKata.Tests.Integration;

public class AccountServicesShould
{
    private readonly TestContext _testContext;
    private readonly AccountServices _accountServices;
    private readonly AccountRepository _accountRepository;
    public AccountServicesShould()
    {
        _testContext = new TestContext();
        _accountRepository = new AccountRepository(_testContext.TestDbContext());
        _accountServices = new AccountServices(_accountRepository);
        Task.WaitAll(_testContext.RespawnDb());
    }

    [Fact]
    public async Task DepositAmountCorrectlyInTheAccount()
    {
        _testContext.BankKataDbContext.Accounts.AddAsync(new Account(new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae"), 0));
        _testContext.BankKataDbContext.Movements.AddAsync(new Movement(Guid.NewGuid(),new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae"), 0, 0));
        _testContext.BankKataDbContext.SaveChangesAsync();
        
        var accountId = new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae");
        var accountRequest = new AccountRequest()
        {
            Id = accountId,
            Amount = 500
        };
        
        await _accountServices.Deposit(accountRequest);
        
        var balance = await _accountRepository.GetBalance(accountId);
        const int expectedBalance = 500;
        balance.Should().Be(expectedBalance);
    }
    
    [Fact]
    public async Task WithdrawAmountCorrectlyInTheAccount()
    {
        _testContext.BankKataDbContext.Accounts.AddAsync(new Account(new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c"), 500));
        _testContext.BankKataDbContext.Movements.AddAsync(new Movement(Guid.NewGuid(),new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c"), 500, 500));
        _testContext.BankKataDbContext.SaveChangesAsync();
        
        var accountId = new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c");
        var accountRequest = new AccountRequest()
        {
            Id = accountId,
            Amount = 500
        };
        
        await _accountServices.Withdraw(accountRequest);
        
        var balance = await _accountRepository.GetBalance(accountId);
        const int expectedBalance = 0;
        balance.Should().Be(expectedBalance);
    }
}