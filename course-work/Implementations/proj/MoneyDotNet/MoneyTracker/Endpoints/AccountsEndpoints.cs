using System.Data;
using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Dtos;
using MoneyTracker.Entities;
using MoneyTracker.Mapping;

namespace MoneyTracker.Endpoints;

public static  class AccountsEndpoints
{
 const string GetAccountEndPoints = "GetAccount";
        public static RouteGroupBuilder MapAccountEndpoints(this WebApplication app)
        {
            //GET 
            var group = app.MapGroup("accounts");
             group.MapGet("/", async (MoneyTrackerContext dbContext) => 
            await dbContext.Accounts
                .Include(account => account.User)
                .Select(account => account.ToDto())
                .ToListAsync());


            //GET{ID}
            group.MapGet("/{id}", (int id, MoneyTrackerContext dbcontext) =>
            {
                Account? account = dbcontext.Accounts.Find(id);

                return account is null ? Results.NotFound() : Results.Ok(account.ToFullDto());
            })
            .WithName(GetAccountEndPoints);

        //POST
        group.MapPost("/", (CreateAccountDto newAccount, MoneyTrackerContext dbContext) =>
        {
            Account account = newAccount.ToEntity();
            dbContext.Accounts.Add(account);
            dbContext.SaveChanges();
          
            return Results.CreatedAtRoute(GetAccountEndPoints, new { id = account.AccountId }, account.ToFullDto());
        });

            //PUT
            group.MapPut("/{id}", (int id, UpdateAccountDto updatedAccount, MoneyTrackerContext dbContext) =>
            {
                var cutAccount = dbContext.Accounts.Find(id);
                if(cutAccount is null){
                    return Results.NotFound();
                }
               dbContext.Entry(cutAccount).CurrentValues.SetValues(updatedAccount.ToEntity(id));
               dbContext.SaveChanges();
                return Results.NoContent();
            });

            //DELETE
            group.MapDelete("/{id}", (int id, MoneyTrackerContext dbContext) =>
            {
                dbContext.Accounts.Where(account => account.AccountId == id).ExecuteDelete();
                return Results.NoContent();

            });
            return group;
    }
}
