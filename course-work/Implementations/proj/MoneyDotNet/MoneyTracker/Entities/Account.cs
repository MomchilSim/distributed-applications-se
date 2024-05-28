using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.Entities;

public class Account
{
    public int AccountId { get; set; }
    public required int UserId  { get; set; }
    public User? User { get; set; }
    public string? AccountName { get; set; }
    public required decimal Balance { get; set; }
    public required string Currency {get; set; }
    public DateTime CreatedDate { get; set; }

    public static explicit operator Account(Transaction v)
    {
        throw new NotImplementedException();
    }
}

