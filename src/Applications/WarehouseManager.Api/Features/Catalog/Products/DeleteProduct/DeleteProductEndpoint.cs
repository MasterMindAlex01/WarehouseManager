using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/products/{id:guid}",
        async (IMediator mediator, Guid id) =>
        {
            return await mediator.Send(new DeleteProductRequest(id));
        })
        .RequirePermission(WHMAction.Delete, WHMResource.Products)
        .WithName("DeleteProduct")
        .WithSummary("Delete a product by ID.")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
