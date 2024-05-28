using MoneyTracker.Dtos;
using MoneyTracker.Entities;

namespace MoneyTracker.Mapping;

public static class AccountMapping
{
    public static Account ToEntity(this CreateAccountDto acc){
            return new Account()
            {
                AccountName = acc.AccountName,
                Balance = acc.Balance,
                UserId = acc.UserId,
                Currency = acc.Currency,
                CreatedDate = DateTime.UtcNow
                
            };
    }
    public static Account ToEntity(this UpdateAccountDto acc, int id){
             return new Account()
            {
                AccountId = id,
                AccountName = acc.AccountName,
                Balance = acc.Balance,
                UserId = acc.UserId,
                Currency = acc.Currency,
                CreatedDate = acc.CreatedDate
            };
    }
    public static AccountSummaryDto ToDto (this Account acc){
                return new(
                acc.AccountId,
                acc.User!.Username,
                acc.AccountName!,
                acc.Balance,
                acc.Currency,
                acc.CreatedDate
                );
    }
    public static AccountFullDto ToFullDto (this Account acc){
                return new(
                acc.AccountId,
                acc.UserId,
                acc.AccountName!,
                acc.Balance,
                acc.Currency,
                acc.CreatedDate
                );
    }
}
