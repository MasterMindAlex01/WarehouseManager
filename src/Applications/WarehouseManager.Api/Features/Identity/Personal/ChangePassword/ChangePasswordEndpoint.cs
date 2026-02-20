using Carter;
using MediatR;
using System.Security.Claims;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class ChangePasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/personal/change-password",
        async (IMediator mediator, ChangePasswordRequest request, HttpContext HttpContext) =>
        {
            if (HttpContext.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authorized.");
            }

            return await mediator.Send(request);
        })            
        .RequireAuthorization()
        .WithName("ChangePassword")
        .WithSummary("Change password of currently logged in user.")
        .WithTags("Personal")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
