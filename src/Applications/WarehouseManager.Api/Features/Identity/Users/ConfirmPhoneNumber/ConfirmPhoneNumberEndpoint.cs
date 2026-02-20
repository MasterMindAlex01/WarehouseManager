using Carter;
using MediatR;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmPhoneNumberEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/confirm-phone-number",
            async (IMediator mediator, [FromQuery] string userId, [FromQuery] string code) =>
                await mediator.Send(new ConfirmPhoneNumberRequest(userId, code)))
        .WithName("ConfirmPhoneNumber")
        .WithSummary("Confirm a user's phone number.")
        .WithTags("Users")
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
