using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class DeleteBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/brands/{id:guid}",
        async (IMediator mediator, Guid id) =>
        {
            return await mediator.Send(new DeleteBrandRequest(id));
        })
        .RequirePermission(WHMAction.Delete, WHMResource.Brands)
        .WithName("DeleteBrand")
        .WithSummary("Delete a brand by ID.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
