using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Tokens;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class RefreshTokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tokens/refresh",
        async (IMediator mediator, 
            RefreshTokenRequest command,
            [FromHeader(Name = MultitenancyConstants.TenantIdName)] string tenant) =>
        {
            return await mediator.Send(command);
        })
        .RequireTenantIdHeader()
        .WithName("RefreshToken")
        .WithSummary("Request an access token using a refresh token.")
        .WithTags("Tokens")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<TokenResponse>>(StatusCodes.Status200OK);
    }
}
