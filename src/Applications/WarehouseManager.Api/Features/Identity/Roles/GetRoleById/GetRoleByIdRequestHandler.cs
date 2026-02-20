using MediatR;
using WarehouseManager.Application.Identity.Roles;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdRequestHandler : IRequestHandler<GetRoleByIdRequest, IResult>
{
    private readonly IRoleService _roleService;

    public GetRoleByIdRequestHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<IResult> Handle(GetRoleByIdRequest request, CancellationToken cancellationToken)
    {
        RoleDto roleDto = await _roleService.GetByIdAsync(request.Id); 
        return Results.Ok(Result<RoleDto>.Success(roleDto, "Ok"));
    }
}
