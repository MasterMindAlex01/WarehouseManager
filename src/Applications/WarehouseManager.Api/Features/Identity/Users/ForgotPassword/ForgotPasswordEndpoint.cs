using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users.ForgotPassword;

public class ForgotPasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/users/forgot-password",
            async (IMediator mediator, ForgotPasswordRequest request,
            [FromHeader(Name = MultitenancyConstants.TenantIdName)] string tenant) =>
                await mediator.Send(request))
        .RequireTenantIdHeader()
        .WithName("ForgotPassword")
        .WithSummary("Request a password reset email for a user.")
        .WithTags("Users")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
