namespace MoneyTracker.Data;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Entities;
public class MoneyTrackerContext(DbContextOptions<MoneyTrackerContext> ops) : DbContext(ops)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new{UserId = 1, Username ="a", Password = "b", Email = "c@c", CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow), DateOfBirth = DateTime.Now});
    }
}
