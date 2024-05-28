using Microsoft.EntityFrameworkCore;
using MoneyTracker.Data;

namespace GameStore.Api.Data;

public static class DataExtensions
{
    public static async Task MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MoneyTrackerContext>();
        await dbContext.Database.MigrateAsync();
    }
}
