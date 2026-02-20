using MediatR;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class DeleteRoleRequestHandler : IRequestHandler<DeleteRoleRequest, IResult>
{
    private readonly IRoleService _roleService;

    public DeleteRoleRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        var response = await _roleService.DeleteAsync(request.Id);
        return Results.Ok(Result<string>.Success(response, "Role deleted successfully"));
    }
}
