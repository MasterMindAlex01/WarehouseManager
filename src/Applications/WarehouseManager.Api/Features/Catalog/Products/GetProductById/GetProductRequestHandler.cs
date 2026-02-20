using Ardalis.Specification;
using MediatR;
using Microsoft.Extensions.Localization;
using WarehouseManager.Application.Catalog.Products;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Domain.Catalog;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductDetailsDto>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<Product> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<ProductDetailsDto> Handle(GetProductRequest request, CancellationToken cancellationToken) =>
        await _repository.FirstOrDefaultAsync(
            (ISpecification<Product, ProductDetailsDto>)new ProductByIdWithBrandSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Product {0} Not Found.", request.Id]);
}