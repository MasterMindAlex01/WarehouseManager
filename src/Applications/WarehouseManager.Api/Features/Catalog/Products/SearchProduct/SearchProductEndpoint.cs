using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Catalog.Products;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class SearchProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/Products/search",
        async (IMediator mediator, SearchProductsRequest request) =>
        {
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Search, WHMResource.Products)
        .WithName("SearchProducts")
        .WithSummary("Search for products.")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<PaginationResponse<ProductDto>>(StatusCodes.Status200OK);
    }
}
