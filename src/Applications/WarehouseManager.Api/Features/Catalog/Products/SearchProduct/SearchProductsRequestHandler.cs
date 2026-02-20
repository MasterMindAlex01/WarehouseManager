using MediatR;
using WarehouseManager.Application.Catalog.Products;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Domain.Catalog;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class SearchProductsRequestHandler : IRequestHandler<SearchProductsRequest, PaginationResponse<ProductDto>>
{
    private readonly IReadRepository<Product> _repository;

    public SearchProductsRequestHandler(IReadRepository<Product> repository) => _repository = repository;

    public async Task<PaginationResponse<ProductDto>> Handle(SearchProductsRequest request, CancellationToken cancellationToken)
    {
        var dto = new SearchProductsRequestDto
        {
            BrandId = request.BrandId,
            MinimumRate = request.MinimumRate,
            MaximumRate = request.MaximumRate,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
        var spec = new ProductsBySearchRequestWithBrandsSpec(dto);
        return await _repository.PaginatedListAsync(spec, dto.PageNumber, dto.PageSize, cancellationToken: cancellationToken);
    }
}