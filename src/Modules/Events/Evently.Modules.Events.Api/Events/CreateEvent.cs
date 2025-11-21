using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Api.Events;

public static class CreateEvent
{
    public static void MapEndPoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (Request request, EventsDbContext dbContext, CancellationToken cancellationToken) =>
        {
            var @event = new Event()
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                StartsAtUtc = request.StartsAtUtc,
                EndsAtUtc = request.EndsAtUtc,
                Status = EventStatus.Draft
            };

            dbContext.Events.Add(@event);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(@event.Id);

        }).WithTags(Tags.Events);
    }

    private sealed class Request
    {
        public string Title{ get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime StartsAtUtc { get; set; }

        public DateTime? EndsAtUtc { get; set; }
    }
}
