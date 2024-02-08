namespace BankKata.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } 
    public int Balance { get; set; }
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();

    public Account() {}

    public Account(Guid id, int balance)
    {
        Id = id;
        Balance = balance;
    }

    public int GetBalance()
    {
        return Balance;
    }
}