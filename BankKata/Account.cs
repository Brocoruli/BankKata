namespace BankKata;

public class Account
{
    public int Id { get; set; } 
    public int Balance { get; set; }
    public ICollection<Movement> Movements { get; set; } = new List<Movement>();

    public Account() {}

    public Account(int id, int balance)
    {
        Id = id;
        Balance = balance;
    }
}