using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Catalog.Brands;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands
{
    public class SearchBrandEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/brands/search",
            async (IMediator mediator, SearchBrandsRequest request) =>
            {
                return await mediator.Send(request);
            })
            .RequirePermission(WHMAction.Search, WHMResource.Brands)
            .WithName("SearchBrands")
            .WithSummary("Search for brands.")
            .WithTags("Brands")
            .ProducesValidationProblem()
            .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
            .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
            .Produces<PaginationResponse<BrandDto>>(StatusCodes.Status200OK);
        }
    }
}
