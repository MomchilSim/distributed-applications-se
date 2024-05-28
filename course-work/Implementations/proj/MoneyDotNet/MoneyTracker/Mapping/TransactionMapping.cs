using MoneyTracker.Dtos;
using MoneyTracker.Entities;

namespace MoneyTracker.Mapping;

public static class TransactionMapping
{
    public static Transaction ToEntity(this CreateTransactionDto trn){
            return new Transaction()
            {
                AccountID = trn.AccountID,
                Amount = trn.Amount,
                Category = trn.Category,
                Description = trn.Description,
                TransactionDate = trn.TransactionDate
            };
    }
    public static Transaction ToEntity(this UpdateTransactionDto trn, int id){
             return new Transaction()
            {
                TransactionID = id,
                AccountID = trn.AccountID,
                Amount = trn.Amount,
                Category = trn.Category,
                Description = trn.Description,
                TransactionDate = trn.TransactionDate
            };
    }
    public static TransactionSummaryDto ToDto (this Transaction trn){
                return new(
                trn.TransactionID,
                trn.Account!.AccountName!,
                trn.Amount,
                trn.Category!,
                trn.Description,
                trn.TransactionDate
                );
    }
    public static TransactionFullDto ToFullDto (this Transaction trn){
                return new(
               trn.TransactionID,
                trn.AccountID!,
                trn.Amount,
                trn.Category!,
                trn.Description,
                trn.TransactionDate
                );
    }
}
