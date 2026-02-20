using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserListEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/users", 
        async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserListRequest());
            return result;
        })
        .RequirePermission(WHMAction.View, WHMResource.Users)
        .WithName("GetUserList")
        .WithSummary("Get list of all users.")
        .WithTags("Users")
        .Produces<Result<List<UserDetailsDto>>>(StatusCodes.Status200OK);
    }
}
