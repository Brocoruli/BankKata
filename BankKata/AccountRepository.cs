using BankKata;

public class AccountRepository
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
    
    public void Withdraw(AccountRequest accountRequest)
    {
        var persistedAccount = Accounts.Find(a => a.Id == accountRequest.Id);
        var result = persistedAccount.Balance - accountRequest.Amount;
        if (result < 0)
        {
            throw new InvalidOperationException("Your balance is less than the amount that you want");;
        }
        persistedAccount.Balance = result;
        persistedAccount.Movements.Add(new Movement(-accountRequest.Amount, persistedAccount.Balance));
    }

    public int GetBalance(int id)
    {
        var persistedAccount = Accounts.Find(a => a.Id == id);
        return persistedAccount.Balance;
    }

    public Account Find(int accountId)
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