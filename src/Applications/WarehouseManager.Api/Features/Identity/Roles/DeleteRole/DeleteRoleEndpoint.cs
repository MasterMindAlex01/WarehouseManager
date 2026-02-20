using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class DeleteRoleEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/roles/{id}",
        async (IMediator mediator, string id) =>
        {
            return await mediator.Send(new DeleteRoleRequest(id));
        })
        .RequirePermission(WHMAction.Delete, WHMResource.Roles)
        .WithName("DeleteRole")
        .WithSummary("Delete a role by Id.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status401Unauthorized)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
