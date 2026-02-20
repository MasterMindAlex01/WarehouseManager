using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class DeleteRandomBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/brands/delete-random",
        async (IMediator mediator) =>
        {
            return await mediator.Send(new DeleteRandomBrandRequest());
        })
        .RequirePermission(WHMAction.Clean, WHMResource.Brands)
        .WithName("DeleteRandomBrand")
        .WithSummary("Delete the brands generated with the generate-random call.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
