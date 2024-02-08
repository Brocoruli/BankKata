namespace BankKata.Domain.Entities;

public class Movement
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    

    public Movement(){}

    public Movement(Guid id, Guid accountId, int amount, int balance)
    {
        Id = id;
        AccountId = accountId;
        Balance = balance;
        Amount = amount;
    }

    public Movement(int amount, int balance)
    {
        Balance = balance;
        Amount = amount;
    }
}