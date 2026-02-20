using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UserRolesRequestHandler : IRequestHandler<UserRolesRequest, IResult>
{
    private readonly IUserService _userService;

    public UserRolesRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(UserRolesRequest request, CancellationToken cancellationToken)
    {
        var userRolesRequestDto = new UserRolesRequestDto
        {
            UserId = request.UserId,
            UserRoles = request.UserRoles
        };
        var result = await _userService.AssignRolesAsync(request.UserId, userRolesRequestDto, cancellationToken);
        return Results.Ok(Result.Success(result));
    }
}