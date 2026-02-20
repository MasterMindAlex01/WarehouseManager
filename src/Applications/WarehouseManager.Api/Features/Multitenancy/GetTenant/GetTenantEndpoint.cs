using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Multitenancy;

namespace WarehouseManager.Api.Features.Multitenancy;

public class GetTenantEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tenants/{id}",
            async (string id, IMediator mediator) =>
            {
                return await mediator.Send(new GetTenantRequest(id));
            })
        .RequirePermission(WHMAction.View, WHMResource.Tenants)
        .WithName("GetTenant")
        .WithSummary("Get tenant by id.")
        .WithTags("Tenants")
        .Produces<TenantDto>(StatusCodes.Status200OK);
    }
}
