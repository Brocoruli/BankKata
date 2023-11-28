using BankKata;

public class AccountRepository
{
    private List<Account> accounts = new List<Account>()
    {
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
            }
        }
    };
    
    public void Deposit(int id, int amount)
    {
        var persistedAccount = accounts.Find(a => a.Id == id);
        persistedAccount.Balance += amount;
        persistedAccount.Movements.Add(new Movement(amount, persistedAccount.Balance));
    }
}