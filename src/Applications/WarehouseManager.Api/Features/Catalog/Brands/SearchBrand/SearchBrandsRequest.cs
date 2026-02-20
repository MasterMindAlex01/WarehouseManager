using Ardalis.Specification;
using MediatR;
using WarehouseManager.Application.Catalog.Brands;
using WarehouseManager.Application.Common.Specification;
using WarehouseManager.Domain.Catalog;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class SearchBrandsRequest : PaginationFilter, IRequest<PaginationResponse<BrandDto>>
{
}

public class BrandsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Brand, BrandDto>
{
    public BrandsBySearchRequestSpec(SearchBrandsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}
