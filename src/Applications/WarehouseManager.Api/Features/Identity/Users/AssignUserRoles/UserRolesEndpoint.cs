using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UserRolesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/{id}/roles",
        async (IMediator mediator, string id, UserRolesRequest command) =>
        {
            if (id != command.UserId) 
            { 
                throw new BadRequestException("User ID mismatch");
            }
            return await mediator.Send(command);
        })
        .RequirePermission(WHMAction.Update, WHMResource.UserRoles)
        .WithName("AssignUserRoles")
        .WithSummary("Update a user's assigned roles.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
