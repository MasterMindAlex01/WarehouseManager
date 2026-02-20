using Carter;
using MediatR;
using System.Security.Claims;
using WarehouseManager.Api.Features.Identity.Users;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class UpdateProfileEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/personal/profile",
        async (IMediator mediator, HttpContext HttpContext, UpdateUserRequest command) =>
        {
            if (HttpContext.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authorized.");
            }

            if (command.Id != userId)
            {
                throw new UnauthorizedException("You are not authorized to update this profile.");
            }

            return await mediator.Send(command);
        })
        .RequireAuthorization()
        .WithName("UpdateProfile")
        .WithSummary("Update profile details of currently logged in user.")
        .WithTags("Personal")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
