using MediatR;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleListRequestHandler : IRequestHandler<GetRoleListRequest, IResult>
{
    private readonly IRoleService _roleService;

    public GetRoleListRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(GetRoleListRequest request, CancellationToken cancellationToken)
    {
        List<RoleDto> roles = await _roleService.GetListAsync(cancellationToken);
        return Results.Ok(Result<List<RoleDto>>.Success(roles));
    }
}
