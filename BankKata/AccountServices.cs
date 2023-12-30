namespace BankKata;

public class AccountServices
{
    private AccountRepository _accountRepository;
    
    public AccountServices(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public void Deposit(AccountRequest accountRequest)
    {
        var account = _accountRepository.Find(accountRequest.Id);
        account.Balance += accountRequest.Amount;
        account.Movements.Add(new Movement(accountRequest.Amount, account.Balance));
        _accountRepository.Save(account);
    }

    public void Withdraw(AccountRequest accountRequest)
    {
        var account = _accountRepository.Find(accountRequest.Id);
        var result = account.Balance - accountRequest.Amount;
        account.Balance = result;
        account.Movements.Add(new Movement(-accountRequest.Amount, account.Balance));
        _accountRepository.Save(account);
    }
}