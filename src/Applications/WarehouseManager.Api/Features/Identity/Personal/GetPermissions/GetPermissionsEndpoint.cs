using Carter;
using MediatR;
using System.Security.Claims;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class GetPermissionsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/personal/permissions",
        async (IMediator mediator, HttpContext HttpContext) =>
        {
            if (HttpContext.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authorized.");
            }

            return await mediator.Send(new GetPermissionsRequest(userId));
        })            
        .RequireAuthorization()
        .WithName("GetPermissions")
        .WithSummary("Get permissions of currently logged in user.")
        .WithTags("Personal")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<Result<List<string>>>(StatusCodes.Status200OK);
    }
}
