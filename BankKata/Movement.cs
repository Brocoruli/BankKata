namespace BankKata;

public class Movement
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    

    public Movement(){}

    public Movement(int id, int accountId, int amount, int balance)
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