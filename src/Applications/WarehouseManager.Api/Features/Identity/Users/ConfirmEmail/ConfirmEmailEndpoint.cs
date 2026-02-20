using Carter;
using MediatR;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmEmailEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/confirm-email",
            async (IMediator mediator, [FromQuery] string tenant, [FromQuery] string userId, [FromQuery] string code) =>
                await mediator.Send(new ConfirmEmailRequest(tenant, userId, code)))
        .WithName("ConfirmEmail")
        .WithSummary("Confirm a user's email address.")
        .WithTags("Users")
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
