using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class UpdateBrandEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/brands/{id:guid}",
        async (IMediator mediator, Guid id, [FromBody] UpdateBrandRequest request) =>
        {
            if (id != request.Id)
            {
                throw new BadRequestException("The ID in the route does not match the ID in the request body.");
            }

            return await mediator.Send(request);

        })
        .RequirePermission(WHMAction.Update, WHMResource.Brands)
        .WithName("UpdateBrand")
        .WithSummary("Update an existing brand.")
        .WithTags("Brands")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);
    }
}
