using BankKata.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankKata.Infrastructure.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BankKataDbContext _context;

    public AccountRepository(BankKataDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetBalance(Guid accountId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        return account.Balance;
    }

    public async Task<Account> Find(Guid accountId)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
        return account;
    }

    public async Task Save(Account account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }
}