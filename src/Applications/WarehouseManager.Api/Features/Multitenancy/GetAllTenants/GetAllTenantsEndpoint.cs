using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Multitenancy;

namespace WarehouseManager.Api.Features.Multitenancy;

public class GetAllTenantsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tenants",
            async (IMediator mediator) =>
            {
                return await mediator.Send(new GetAllTenantsRequest());
            })
        .RequirePermission(WHMAction.View, WHMResource.Tenants)
        .WithName("GetAllTenants")
        .WithSummary("Get all tenants.")
        .WithTags("Tenants")
        .Produces<List<TenantDto>>(StatusCodes.Status200OK);
    }
}
