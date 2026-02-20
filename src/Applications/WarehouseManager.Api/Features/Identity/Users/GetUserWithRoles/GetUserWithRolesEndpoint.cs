using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserWithRolesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/{id}/roles", 
        async (IMediator mediator, string id) =>
        {
            var result = await mediator.Send(new GetUserWithRolesRequest(id));
            return result;
        })
        .RequirePermission(WHMAction.View, WHMResource.UserRoles)
        .WithName("GetUserWithRoles")
        .WithSummary("Get user with roles by ID.")
        .WithTags("Users")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)    
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<List<UserRoleDto>>>(StatusCodes.Status200OK);
    }
}
