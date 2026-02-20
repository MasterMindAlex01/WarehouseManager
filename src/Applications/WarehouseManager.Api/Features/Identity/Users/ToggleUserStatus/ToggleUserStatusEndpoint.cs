using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ToggleUserStatusEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/{id}/toggle-status",
        async (IMediator mediator, string id, ToggleUserStatusRequest request) =>
        {
            if (id != request.UserId)
            {
                throw new BadRequestException("User ID mismatch");
            }
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Update, WHMResource.Users)
        .WithName("ToggleUserStatus")
        .WithSummary("Toggle user status by ID.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
