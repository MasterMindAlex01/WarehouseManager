using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserWithRolesRequestHandler : IRequestHandler<GetUserWithRolesRequest, IResult>
{
    private readonly IUserService _userService;

    public GetUserWithRolesRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(GetUserWithRolesRequest request, CancellationToken cancellationToken)
    {
        List<UserRoleDto> userWithRoles = await _userService.GetRolesAsync(request.Id, cancellationToken);
        return Results.Ok(Result<List<UserRoleDto>>.Success(userWithRoles, "Ok"));
    }
}