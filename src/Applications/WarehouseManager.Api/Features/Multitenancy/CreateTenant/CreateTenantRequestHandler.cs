using MediatR;
using WarehouseManager.Application.Common.Exceptions;
using WarehouseManager.Application.Common.Persistence;
using WarehouseManager.Application.Multitenancy;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Multitenancy.CreateTenant;

public class CreateTenantRequestHandler : IRequestHandler<CreateTenantRequest, IResult>
{
    private readonly ITenantService _tenantService;
    private readonly IConnectionStringValidator _connectionStringValidator;

    public CreateTenantRequestHandler(
        ITenantService tenantService, 
        IConnectionStringValidator connectionStringValidator)
    {
        _tenantService = tenantService;
        _connectionStringValidator = connectionStringValidator;
    }

    public async Task<IResult> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
    {
        if (await _tenantService.ExistsWithIdAsync(request.Id))
            throw new ConflictException($"Tenant {request.Id} already exists.");
        if (await _tenantService.ExistsWithNameAsync(request.Name))
            throw new ConflictException($"Tenant {request.Id} already exists.");
        if (string.IsNullOrWhiteSpace(request.ConnectionString) 
            || !_connectionStringValidator.TryValidate(request.ConnectionString))
            throw new ConflictException("Connection string invalid.");
        var createTenantRequestDto = new CreateTenantRequestDto
        {
            Id = request.Id,
            Name = request.Name,
            ConnectionString = request.ConnectionString,
            AdminEmail = request.AdminEmail,
            Issuer = request.Issuer,
        };
        var response = await _tenantService.CreateAsync(createTenantRequestDto, cancellationToken);
        
        return Results.Ok(Result<string>.Success(response, "Create tenant successful"));
    }
}