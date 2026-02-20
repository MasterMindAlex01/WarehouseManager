using Carter;
using MediatR;
using WarehouseManager.Api.Features.Multitenancy;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;
using WarehouseManager.Shared.Wrapper;

namespace SailboatManagement.Application.Core.Features.Multitenancy;

public class UpgradeSubscriptionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tenants/{id}/upgrade",
            async (IMediator mediator, string id, UpgradeSubscriptionRequest command) =>
            {
                if (id != command.TenantId)
                {
                    throw new BadRequestException("Tenant ID mismatch");
                }
                return await mediator.Send(command);
            })
        .RequirePermission(WHMAction.UpgradeSubscription, WHMResource.Tenants)
        .WithName("UpgradeSubscription")
        .WithSummary("Upgrade tenant subscription.")
        .WithTags("Tenants")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status404NotFound)
        .Produces<ErrorResult>(StatusCodes.Status409Conflict)
        .Produces<Result>(StatusCodes.Status200OK);
    }
}
