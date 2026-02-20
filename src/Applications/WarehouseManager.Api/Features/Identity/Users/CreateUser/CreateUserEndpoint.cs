using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users",
            async (IMediator mediator, CreateUserRequest command) =>
            {
                return await mediator.Send(command);
            })
        .RequirePermission(WHMAction.Create, WHMResource.Users)
        .WithName("CreateUser")
        .WithSummary("Create a new user.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
