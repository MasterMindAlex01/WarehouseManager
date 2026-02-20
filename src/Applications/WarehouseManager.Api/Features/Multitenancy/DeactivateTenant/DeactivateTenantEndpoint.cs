using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy;

public class DeactivateTenantEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tenants/{id}/deactivate",
            async (IMediator mediator, string id) =>
            {
                return await mediator.Send(new DeactivateTenantRequest(id));
            })
        .RequirePermission(WHMAction.Update, WHMResource.Tenants)
        .WithName("DeactivateTenant")
        .WithSummary("Deactivate a tenant.")
        .WithTags("Tenants")
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
