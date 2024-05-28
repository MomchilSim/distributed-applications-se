using GameStore.Api.Data;
using MoneyTracker.Authentication;
using MoneyTracker.Data;
using MoneyTracker.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString =  builder.Configuration.GetConnectionString("whatever");
builder.Services.AddSqlite<MoneyTrackerContext>(connString);

var app = builder.Build();

app.MapUsersEndpoints();
app.MapAccountEndpoints();
app.MapTransactionsEndpoints();


//app.UseMiddleware<ApiAuthMiddleWare>();
//app.UseAuthorization();

await app.MigrateDb();
app.Run();
