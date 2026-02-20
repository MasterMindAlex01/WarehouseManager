using MediatR;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class UpdateRolePermissionsRequestHandler : IRequestHandler<UpdateRolePermissionsRequest, IResult>
{
    private readonly IRoleService _roleService;

    public UpdateRolePermissionsRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var updateRolePermissionsDto = new UpdateRolePermissionsRequestDto
        {
            RoleId = request.RoleId,
            Permissions = request.Permissions
        };
        var response = await _roleService.UpdatePermissionsAsync(updateRolePermissionsDto, cancellationToken);
        return Results.Ok(Result<string>.Success(response, "Update permissions successful"));
    }
}
