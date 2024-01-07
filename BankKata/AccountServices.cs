namespace BankKata;

public class AccountServices
{
    private IAccountRepository _accountRepository;
    
    public AccountServices(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async void Deposit(AccountRequest accountRequest)
    {
        var account = await _accountRepository.Find(accountRequest.Id);
        account.Balance += accountRequest.Amount;
        account.Movements.Add(new Movement(accountRequest.Amount, account.Balance));
        _accountRepository.Save(account);
    }

    public async void Withdraw(AccountRequest accountRequest)
    {
        var account = await _accountRepository.Find(accountRequest.Id);
        var result = account.Balance - accountRequest.Amount;
        account.Balance = result;
        account.Movements.Add(new Movement(-accountRequest.Amount, account.Balance));
        _accountRepository.Save(account);
    }
}