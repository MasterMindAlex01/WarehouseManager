using Carter;
using MediatR;
using WarehouseManager.Api.Filters;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Infrastructure.Middleware;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products/{id:guid}",
        async (IMediator mediator, Guid id, [FromBody] UpdateProductRequest request) =>
        {
            if (id != request.Id)
            {
                throw new BadRequestException("The ID in the route does not match the ID in the request body.");
            }

            return await mediator.Send(request);

        })
        .RequirePermission(WHMAction.Update, WHMResource.Products)
        .WithName("UpdateProduct")
        .WithSummary("Update an existing product.")
        .WithTags("Products")
        .ProducesValidationProblem()
        .Produces<ErrorResult>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResult>(StatusCodes.Status500InternalServerError)
        .Produces<Guid>(StatusCodes.Status200OK);

    }
}
