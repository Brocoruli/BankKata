using BankKata.Domain.Entities;

namespace BankKata.Tests.Repositories;

public class AccountRepositoryOnMemory : IAccountRepository
{
    private List<Account> Accounts = new()
    {
        new Account()
        {
            Id = new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae"),
            Balance = 0,
            Movements = {
                {
                    new Movement(Guid.NewGuid(), new Guid("881b2297-1c8f-4ef2-b80c-bfa5a43107ae"),0,0)
                }
            }
        },
        new Account()
        {
            Id = new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c"),
            Balance = 500,
            Movements = {
                {
                    new Movement(Guid.NewGuid(), new Guid("2d61906c-d856-4b3b-89b1-67673ee5499c"),500,500)
                }
            }
        }
    };

    public async Task<Account> Find(Guid accountId)
    {
        return Accounts.Find(a => a.Id == accountId);
    }

    public async Task Save(Account account)
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