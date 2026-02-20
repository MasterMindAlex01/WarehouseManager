using MediatR;
using WarehouseManager.Application.Common.Interfaces;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Application.Identity.Users.Password;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class ChangePasswordRequestHandler : IRequestHandler<ChangePasswordRequest, IResult>
{
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public ChangePasswordRequestHandler(
        ICurrentUser currentUser,
        IUserService userService)
    {
        _currentUser = currentUser;
        _userService = userService;
    }

    public async Task<IResult> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var changePassword = new ChangePasswordRequestDto
        {
            Password = request.Password,
            NewPassword = request.NewPassword,
            ConfirmNewPassword = request.ConfirmNewPassword
        };
        await _userService.ChangePasswordAsync(changePassword, _currentUser.GetUserId().ToString());

        return Results.Ok(Result.Success("Password changed successfully."));
    }
}
