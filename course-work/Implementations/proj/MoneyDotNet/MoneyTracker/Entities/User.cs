namespace MoneyTracker.Entities;

public class User
{
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }   
    public string? Email  { get; set; }
    public DateOnly CreatedDate { get; set; }
    public DateTime DateOfBirth { get; set; }
}
