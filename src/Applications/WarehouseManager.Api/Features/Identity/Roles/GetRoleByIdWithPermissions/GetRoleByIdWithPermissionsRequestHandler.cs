using MediatR;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdWithPermissionsRequestHandler : IRequestHandler<GetRoleByIdWithPermissionsRequest, IResult>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdWithPermissionsRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(
        GetRoleByIdWithPermissionsRequest request, 
        CancellationToken cancellationToken)
    {
        RoleDto roleWithPermissions = await _roleService
            .GetByIdWithPermissionsAsync(request.Id, cancellationToken);
        return Results.Ok(Result<RoleDto>.Success(roleWithPermissions));
    }
}
