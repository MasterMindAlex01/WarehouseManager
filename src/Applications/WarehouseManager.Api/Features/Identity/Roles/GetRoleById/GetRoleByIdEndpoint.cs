using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/roles/{id}",
        async (IMediator mediator, string id) =>
        {
            return await mediator.Send(new GetRoleByIdRequest(id));
        })
        .RequirePermission(WHMAction.View, WHMResource.Roles)
        .WithName("GetRoleById")
        .WithSummary("Get role details by Id.")
        .WithTags("Roles")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status401Unauthorized)
        .Produces<Result<RoleDto>>(StatusCodes.Status200OK);
    }
}
