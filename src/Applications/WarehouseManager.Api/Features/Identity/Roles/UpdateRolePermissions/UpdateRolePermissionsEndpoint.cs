using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class UpdateRolePermissionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/roles/{id}/permissions",
        async (IMediator mediator, string id, UpdateRolePermissionsRequest request) =>
        {
            if (id != request.RoleId)
            {
                throw new BadRequestException("Role ID in the URL does not match Role ID in the request body.");
            }
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Update, WHMResource.RoleClaims)
        .WithName("UpdateRolePermissions")
        .WithSummary("Update role permissions.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status401Unauthorized)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
