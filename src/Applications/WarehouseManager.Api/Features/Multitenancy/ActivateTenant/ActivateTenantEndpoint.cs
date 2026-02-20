using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class ActivateTenantEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tenants/{id}/activate",
            async (IMediator mediator, string id) =>
            {
                return await mediator.Send(new ActivateTenantRequest(id));
            })
        .RequirePermission(WHMAction.Update, WHMResource.Tenants)
        .WithName("ActivateTenant")
        .WithSummary("Activate a tenant.")
        .WithTags("Tenants")
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
