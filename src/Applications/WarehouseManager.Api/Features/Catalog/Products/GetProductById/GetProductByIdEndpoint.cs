using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Catalog.Products;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{id:guid}",
        async (IMediator mediator, Guid id) =>
        {
            return await mediator.Send(new GetProductRequest(id));
        })
        .RequirePermission(WHMAction.View, WHMResource.Products)
        .WithName("GetProductById")
        .WithSummary("Get a product by ID.")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<ProductDetailsDto>(StatusCodes.Status200OK);
    }
}
