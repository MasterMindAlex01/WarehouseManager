using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleListEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/roles",
        async (IMediator mediator) =>
        {
            return await mediator.Send(new GetRoleListRequest());
        })
        .RequirePermission(WHMAction.View, WHMResource.Roles)
        .WithName("GetRoleList")
        .WithSummary("Get a list of all roles.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status401Unauthorized)
        .Produces<Result<List<RoleDto>>>(StatusCodes.Status200OK);
    }
}
