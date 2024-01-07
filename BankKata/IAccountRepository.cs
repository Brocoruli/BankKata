namespace BankKata;

public interface IAccountRepository
{
    int GetBalance(int id);
    Account Find(int accountId);
    void Save(Account account);
}