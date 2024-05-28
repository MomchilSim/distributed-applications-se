namespace MoneyTracker.Entities;

public class Transaction
{
    public int TransactionID  { get; set; }
    public required int AccountID  { get; set; }
    public Account? Account {get; set;}
    public decimal Amount { get; set; }
    public string? Category {get; set; }
    public string? Description  { get; set; }
    public DateTime TransactionDate { get; set; }
}

