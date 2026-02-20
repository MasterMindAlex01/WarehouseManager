using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/{id}", 
        async (IMediator mediator, string id) =>
        {
            var result = await mediator.Send(new GetUserByIdRequest(id));
            return result;
        })
        .RequirePermission(WHMAction.View, WHMResource.Users)
        .WithName("GetUserById")
        .WithSummary("Get user by ID.")
        .WithTags("Users")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)   
        .Produces<Result<UserDetailsDto>>(StatusCodes.Status200OK);
    }
}
