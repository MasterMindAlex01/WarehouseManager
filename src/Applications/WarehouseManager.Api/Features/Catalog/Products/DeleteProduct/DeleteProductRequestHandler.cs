using MediatR;
using Microsoft.Extensions.Localization;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Domain.Catalog;
using WarehouseManager.Domain.Common.Events;

namespace WarehouseManager.Api.Features.Catalog.Products;

public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IStringLocalizer _t;

    public DeleteProductRequestHandler(
        IRepository<Product> repository, 
        IStringLocalizer<DeleteProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["Product {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));

        await _repository.DeleteAsync(product, cancellationToken);

        return request.Id;
    }
}