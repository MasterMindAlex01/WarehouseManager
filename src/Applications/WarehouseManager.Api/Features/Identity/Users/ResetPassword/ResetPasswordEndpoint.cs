using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ResetPasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/reset-password",
            async (IMediator mediator, ResetPasswordRequest request,
            [FromHeader(Name = MultitenancyConstants.TenantIdName)] string tenant) =>
                await mediator.Send(request))
        .RequireTenantIdHeader()
        .WithName("ResetPassword")
        .WithSummary("Reset a user's password.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
