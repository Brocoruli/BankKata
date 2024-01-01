namespace BankKata;

public class Movement
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public string Statement { get; set; }
    
    public Movement(){}
    
    public Movement(int amount, int balance)
    {
        Statement = $" || {amount} || {balance}";
    }
}