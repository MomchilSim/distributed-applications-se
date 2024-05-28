using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;
using MoneyTracker.Dtos;
using MoneyTracker.Entities;
using MoneyTracker.Mapping;

namespace MoneyTracker.Endpoints;

public static class UsersEndpoints
{
    const string GetuserEndPoints = "GetUser";
        public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("users");
            group.MapGet("/", async (MoneyTrackerContext dbContext) => 
            await dbContext.Users
                .Select(user => user.ToDto())
                .AsNoTracking()
                .ToListAsync());
            //GET{ID}
             group.MapGet("/{id}", (int id, MoneyTrackerContext dbcontext) =>
            {
                User? user = dbcontext.Users.Find(id);

                return user is null ? Results.NotFound() : Results.Ok(user.ToDto());
            })
            .WithName(GetuserEndPoints);


            //POST
            group.MapPost("/", (CreateUserDto newUser, MoneyTrackerContext dbContext) =>
        {
            User user = newUser.ToEntity();
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
          
            return Results.CreatedAtRoute(GetuserEndPoints, new { id = user.UserId }, user.ToDto());
        });

            //PUT
            group.MapPut("/{id}", (int id, UpdateUserDto updatedUser, MoneyTrackerContext dbContext) =>
            {
                var curUser = dbContext.Users.Find(id);
                if(curUser is null){
                    return Results.NotFound();
                }
               dbContext.Entry(curUser).CurrentValues.SetValues(updatedUser.ToEntity(id));
               dbContext.SaveChanges();
               return Results.NoContent();
            });Results.NoContent();

            //DELETE
             group.MapDelete("/{id}", (int id, MoneyTrackerContext dbContext) =>
            {
                dbContext.Users.Where(user => user.UserId == id).ExecuteDelete();
                return Results.NoContent();

            });
            return group;
    }
}
