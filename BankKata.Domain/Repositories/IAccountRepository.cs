using BankKata.Domain.Entities;

namespace BankKata;

public interface IAccountRepository
{
    Task<Account> Find(Guid accountId);
    Task Save(Account account);
}