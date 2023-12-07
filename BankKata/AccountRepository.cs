using BankKata;

public class AccountRepository
{
    private List<Account> accounts = new()
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
    
    public void Deposit(int id, int amount)
    {
        var persistedAccount = accounts.Find(a => a.Id == id);
        persistedAccount.Balance += amount;
        persistedAccount.Movements.Add(new Movement(amount, persistedAccount.Balance));
    }
    
    public void Withdraw(int id, int amount)
    {
        var persistedAccount = accounts.Find(a => a.Id == id);
        persistedAccount.Balance -= amount;
        persistedAccount.Movements.Add(new Movement(-amount, persistedAccount.Balance));
    }

    public int GetBalance(int id)
    {
        var persistedAccount = accounts.Find(a => a.Id == id);
        return persistedAccount.Balance;
    }
}