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
        throw new NotImplementedException();
    }
}