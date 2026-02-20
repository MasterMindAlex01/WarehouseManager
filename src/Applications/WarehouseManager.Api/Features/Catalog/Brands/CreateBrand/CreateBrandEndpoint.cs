using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class CreateBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/brands",
        async (IMediator mediator, CreateBrandRequest request) =>
        {
            return await mediator.Send(request);
        })
        .RequirePermission(WHMAction.Create, WHMResource.Brands)
        .WithName("CreateBrand")
        .WithSummary("Create a new brand.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
