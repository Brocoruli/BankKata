namespace BankKata;

public class Account
{
    public int Balance { get; set; }
    public readonly List<Movement> Movements = new();
}