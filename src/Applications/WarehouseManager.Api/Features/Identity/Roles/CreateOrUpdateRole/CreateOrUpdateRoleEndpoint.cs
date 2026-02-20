using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class CreateOrUpdateRoleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/roles",
        async (IMediator mediator, CreateOrUpdateRoleRequest command) =>
        {
            return await mediator.Send(command);
        })
        .RequirePermission(WHMAction.Create, WHMResource.Roles)
        .WithName("CreateOrUpdateRole")
        .WithSummary("Create a new role.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
