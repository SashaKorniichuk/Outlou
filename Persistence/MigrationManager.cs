using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Persistence;

public static class MigrationManager
{
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder webApp)
    {
        using var scope = webApp.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        dbContext.Database.Migrate();
        
        return webApp;
    }
}