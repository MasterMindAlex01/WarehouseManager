using MediatR;
using Microsoft.Extensions.Localization;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Domain.Catalog;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class UpdateBrandRequestHandler : IRequestHandler<UpdateBrandRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Brand> _repository;
    private readonly IStringLocalizer _t;

    public UpdateBrandRequestHandler(IRepositoryWithEvents<Brand> repository, IStringLocalizer<UpdateBrandRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
    {
        var brand = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = brand
        ?? throw new NotFoundException(_t["Brand {0} Not Found.", request.Id]);

        brand.Update(request.Name, request.Description);

        await _repository.UpdateAsync(brand, cancellationToken);

        return request.Id;
    }
}