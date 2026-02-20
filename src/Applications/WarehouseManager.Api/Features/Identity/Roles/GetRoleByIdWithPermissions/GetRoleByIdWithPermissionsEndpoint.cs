using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdWithPermissionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/roles/{id}/permissions",
        async (IMediator mediator, string id) =>
        {
            return await mediator.Send(new GetRoleByIdWithPermissionsRequest(id));
        })
        .RequirePermission(WHMAction.View, WHMResource.RoleClaims)
        .WithName("GetRoleByIdWithPermissions")
        .WithSummary("Get role details with permissions by Id.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status401Unauthorized)
        .Produces<Result<RoleDto>>(StatusCodes.Status200OK);
    }
}
