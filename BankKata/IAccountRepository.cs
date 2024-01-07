namespace BankKata;

public interface IAccountRepository
{
    Task<int> GetBalance(int id);
    Task<Account> Find(int accountId);
    void Save(Account account);
}