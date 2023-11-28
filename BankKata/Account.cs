namespace BankKata;

public class Account
{
    public int Id { get; set; } 
    public int Balance { get; set; }
    public readonly List<Movement> Movements = new();
}