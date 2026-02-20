using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UpdateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/users/{id}",
            async (IMediator mediator, string id, UpdateUserRequest command) =>
            {
                if (id != command.Id)
                {
                    throw new BadRequestException("User ID mismatch");
                }
                return await mediator.Send(command);
            })
        .RequirePermission(WHMAction.Update, WHMResource.Users)
        .WithName("UpdateUser")
        .WithSummary("Update an existing user.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
