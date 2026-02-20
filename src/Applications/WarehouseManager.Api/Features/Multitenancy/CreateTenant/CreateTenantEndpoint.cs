using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy.CreateTenant;

public class CreateTenantEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tenants",
            async (IMediator mediator, CreateTenantRequest command) =>
            {
                return await mediator.Send(command);
            })
        .RequirePermission(WHMAction.Create, WHMResource.Tenants)
        .WithName("CreateTenant")
        .WithSummary("Create new tenant.")
        .WithTags("Tenants")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result<string>>(StatusCodes.Status200OK);
    }
}
