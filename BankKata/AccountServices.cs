namespace BankKata;

public class AccountServices
{
    private IAccountRepository _accountRepositoryOnMemory;
    
    public AccountServices(IAccountRepository accountRepositoryOnMemory)
    {
        _accountRepositoryOnMemory = accountRepositoryOnMemory;
    }

    public void Deposit(AccountRequest accountRequest)
    {
        var account = _accountRepositoryOnMemory.Find(accountRequest.Id);
        account.Balance += accountRequest.Amount;
        account.Movements.Add(new Movement(accountRequest.Amount, account.Balance));
        _accountRepositoryOnMemory.Save(account);
    }

    public void Withdraw(AccountRequest accountRequest)
    {
        var account = _accountRepositoryOnMemory.Find(accountRequest.Id);
        var result = account.Balance - accountRequest.Amount;
        account.Balance = result;
        account.Movements.Add(new Movement(-accountRequest.Amount, account.Balance));
        _accountRepositoryOnMemory.Save(account);
    }
}