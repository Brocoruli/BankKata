namespace BankKata;

public class Movement
{
    public string Statement { get; set; }
    public Movement(int amount, int balance)
    {
        Statement = $" || {amount} || {balance}";
    }
}