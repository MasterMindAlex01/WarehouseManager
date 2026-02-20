using MediatR;
using WarehouseManager.Application.Catalog.Products;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class GetProductRequest : IRequest<ProductDetailsDto>
{
    public Guid Id { get; set; }

    public GetProductRequest(Guid id) => Id = id;
}
