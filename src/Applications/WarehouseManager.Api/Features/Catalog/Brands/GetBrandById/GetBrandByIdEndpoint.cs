using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Catalog.Brands;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class GetBrandByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/brands/{id:guid}",
        async (IMediator mediator, Guid id) =>
        {
            return await mediator.Send(new GetBrandRequest(id));
        })
        .RequirePermission(WHMAction.View, WHMResource.Brands)
        .WithName("GetBrandById")
        .WithSummary("Get a brand by ID.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<BrandDto>(StatusCodes.Status200OK);
    }
}
