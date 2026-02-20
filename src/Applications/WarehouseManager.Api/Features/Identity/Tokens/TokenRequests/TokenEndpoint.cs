using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Identity.Tokens;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Tokens;

public class TokenEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tokens",
        async (IMediator mediator,
            TokenRequest command, 
            [FromHeader(Name = MultitenancyConstants.TenantIdName)] string tenant) =>
        {
            return await mediator.Send(command);
        })
        .RequireTenantIdHeader()
        .WithName("GetToken")
        .WithSummary("Request an access token using credentials.")
        .WithTags("Tokens")
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<TokenResponse>>(StatusCodes.Status200OK);
    }
}
