namespace BankKata;

public class AccountRepository : IAccountRepository
{
    private readonly BankKataDbContext _context;

    public AccountRepository(BankKataDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetBalance(int accountId)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.Id == accountId);
        return account.Balance;
    }

    public async Task<Account> Find(int accountId)
    {
        var account = _context.Accounts.FirstOrDefault(account => account.Id == accountId);
        return account;
    }

    public async void Save(Account account)
    {
        _context.Update(account);
        await _context.SaveChangesAsync();
    }
}