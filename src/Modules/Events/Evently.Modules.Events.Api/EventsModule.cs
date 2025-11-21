using Evently.Modules.Events.Api.Database;
using Evently.Modules.Events.Api.Events;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Events.Api;

public static class EventsModule
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddEventModule(IConfiguration configuration)
        {
            var databaseConnectionString = configuration.GetConnectionString("database")!;
            
            services.AddDbContext<EventsDbContext>(options =>
                options
                    .UseNpgsql(
                        databaseConnectionString,
                        npgsqlOptions => npgsqlOptions
                            .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Events))
                    .UseSnakeCaseNamingConvention());

            return services;
        }
    }
    
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        CreateEvent.MapEndPoint(app);
    }


}
