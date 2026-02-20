using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class GenerateRandomBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/brands/generate-random",
        async (IMediator mediator, GenerateRandomBrandRequest request) =>
        {
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Generate, WHMResource.Brands)
        .WithName("GenerateRandomBrand")
        .WithSummary("Generate a number of random brands.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<string>(StatusCodes.Status200OK);
    }
}
