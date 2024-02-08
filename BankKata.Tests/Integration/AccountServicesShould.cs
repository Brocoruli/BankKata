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
        var accountId = new Guid("211c1965-5be2-40c7-a83a-fe3d15814d18");
        _testContext.BankKataDbContext.Accounts.AddAsync(new Account(accountId, 0));
        _testContext.BankKataDbContext.Movements.AddAsync(new Movement(Guid.NewGuid(),accountId, 0, 0));
        _testContext.BankKataDbContext.SaveChangesAsync();
        
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
        var accountId = new Guid("5837b26e-48f3-4842-b986-8179dc309a2a");
        _testContext.BankKataDbContext.Accounts.AddAsync(new Account(accountId, 500));
        _testContext.BankKataDbContext.Movements.AddAsync(new Movement(Guid.NewGuid(),accountId, 500, 500));
        _testContext.BankKataDbContext.SaveChangesAsync();
        
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