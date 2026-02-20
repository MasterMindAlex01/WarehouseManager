using Ardalis.Specification;
using MediatR;
using WarehouseManager.Application.Catalog.Brands;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class GetBrandRequest : IRequest<BrandDto>
{
    public Guid Id { get; set; }

    public GetBrandRequest(Guid id) => Id = id;
}
