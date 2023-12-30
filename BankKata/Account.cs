namespace BankKata;

public class Account
{
    public int Id { get; set; } 
    public int Balance { get; set; }
    public List<Movement> Movements = new();
}