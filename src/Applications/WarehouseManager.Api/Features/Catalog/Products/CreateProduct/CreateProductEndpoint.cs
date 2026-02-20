using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products",
        async (IMediator mediator, CreateProductRequest request) =>
        {
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Create, WHMResource.Products)
        .WithName("CreateProduct")
        .WithSummary("Create a new product.")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
