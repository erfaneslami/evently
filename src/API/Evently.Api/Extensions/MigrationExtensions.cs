using Evently.Modules.Events.Api.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Api.Extensions;

internal static class MigrationExtensions
{
    extension(IApplicationBuilder app)
    {
        internal void ApplyMigrations()
        {
            using var scope = app.ApplicationServices.CreateScope();
            
            ApplyMigration<EventsDbContext>(scope);
        }

        private static void ApplyMigration<TDbContext>(IServiceScope serviceScope)
            where TDbContext : DbContext
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
