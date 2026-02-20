using MediatR;

namespace WarehouseManager.Api.Features.Multitenancy.CreateTenant;

public class CreateTenantRequest : IRequest<IResult>
{
    public string Id { get; set; } = default!;
    public string Identifier { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? ConnectionString { get; set; }
    public string AdminEmail { get; set; } = default!;
    public string? Issuer { get; set; }
}
