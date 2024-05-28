using System.Data;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Dtos;
using MoneyTracker.Entities;
using MoneyTracker.Mapping;

namespace MoneyTracker.Endpoints;
public static class TransactionsEndpoints
{
    const string GetTransactonEndPoints = "GetTransaction";
        public static RouteGroupBuilder MapTransactionsEndpoints(this WebApplication app)
        {
            //GET 
            var group = app.MapGroup("transactions");
             group.MapGet("/", (MoneyTrackerContext dbContext) => 
             dbContext.Transactions
             .Include(a => a.Account)
             .Select(a => a.ToDto())
             .ToList());

                
            //GET{ID}
             group.MapGet("/{id}", (int id, MoneyTrackerContext dbcontext) =>
            {
                Transaction? transaction = dbcontext.Transactions.Find(id);

                return transaction is null ? Results.NotFound() : Results.Ok(transaction.ToFullDto());
            })
            .WithName(GetTransactonEndPoints);


            //POST
            group.MapPost("/", (CreateTransactionDto newTransaction, MoneyTrackerContext dbContext) =>
            {
            Transaction transaction = newTransaction.ToEntity();
            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
          
            return Results.CreatedAtRoute(GetTransactonEndPoints, new { id = transaction.TransactionID }, transaction.ToFullDto());
             });

            //PUT
            group.MapPut("/{id}", (int id, UpdateTransactionDto updatedTransaction, MoneyTrackerContext dbContext) =>
            {
                var cutTransaction = dbContext.Transactions.Find(id);
                if(cutTransaction is null){
                    return Results.NotFound();
                }
               dbContext.Entry(cutTransaction).CurrentValues.SetValues(updatedTransaction.ToEntity(id));
               dbContext.SaveChanges();
                return Results.NoContent();
            });Results.NoContent();

            //DELETE
             group.MapDelete("/{id}", (int id, MoneyTrackerContext dbContext) =>
            {
                dbContext.Transactions.Where(transaction => transaction.TransactionID == id).ExecuteDelete();
                return Results.NoContent();

            });
            return group;
    }
}

