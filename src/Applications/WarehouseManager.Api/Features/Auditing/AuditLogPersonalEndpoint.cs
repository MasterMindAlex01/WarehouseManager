using Carter;
using MediatR;
using System.Security.Claims;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Auditing;

public class AuditLogPersonalEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/personal/logs",
        async (IMediator mediator, HttpContext HttpContext) =>
        {
            if (HttpContext.User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedException("User is not authorized.");
            }

            return await mediator.Send(new GetMyAuditLogsRequest());
        })
        .RequireAuthorization()
        .WithName("GetAuditLogsPersonal")
        .WithSummary("Get audit logs of currently logged in user.")
        .WithTags("Personal")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<Result<UserDetailsDto>>(StatusCodes.Status200OK);
    }
}
