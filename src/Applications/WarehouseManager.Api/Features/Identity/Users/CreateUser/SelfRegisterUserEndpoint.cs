using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users.CreateUser;

public class SelfRegisterUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/self-register",
        async (IMediator mediator, CreateUserRequest command,
            [FromHeader(Name = MultitenancyConstants.TenantIdName)] string tenant) =>
        {
            return await mediator.Send(command);
        })
        .RequireTenantIdHeader()
        .WithName("SelfRegisterUser")
        .WithSummary("Anonymous user creates a new user.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}