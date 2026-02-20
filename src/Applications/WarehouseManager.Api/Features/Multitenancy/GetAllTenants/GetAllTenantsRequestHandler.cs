using MediatR;
using WarehouseManager.Api.Features.Multitenancy;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace SailboatManagement.Application.Core.Features.Multitenancy;

public class GetAllTenantsRequestHandler : IRequestHandler<GetAllTenantsRequest, IResult>
{
    private readonly ITenantService _tenantService;

    public GetAllTenantsRequestHandler(ITenantService tenantService) => _tenantService = tenantService;

    public async Task<IResult> Handle(GetAllTenantsRequest request, CancellationToken cancellationToken)
    {
        var tenantDtos = await _tenantService.GetAllAsync();
        return Results.Ok(Result<List<TenantDto>>.Success(tenantDtos));
    }
}