namespace BankKata;

public class AccountRepositoryOnMemory : IAccountRepository
{
    private List<Account> Accounts = new()
    {
        new Account()
        {
            Id = 1,
            Balance = 0,
            Movements = {
                {
                    new Movement(0,0)
                }
            }
        },
        new Account()
        {
            Id = 2,
            Balance = 500,
            Movements = {
                {
                    new Movement(500,500)
                }
            }
        }
    };

    public async Task<int> GetBalance(int id)
    {
        var persistedAccount = Accounts.Find(a => a.Id == id);
        return persistedAccount.Balance;
    }

    public async Task<Account> Find(int accountId)
    {
        return Accounts.Find(a => a.Id == accountId);
    }

    public void Save(Account account)
    {
        var persistedAccount = Accounts.FirstOrDefault(a => a.Id == account.Id);
        if (persistedAccount != null)
        {
            persistedAccount.Balance = account.Balance;
            persistedAccount.Movements = account.Movements;
        }
        else
        {
            Accounts.Add(account);
        }
        
    }
}