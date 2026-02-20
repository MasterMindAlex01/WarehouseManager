using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Application.Identity.Users.Password;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users.ResetPassword;

public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, IResult>
{
    private readonly IUserService _userService;

    public ResetPasswordRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var resetPasswordRequest = new ResetPasswordRequestDto
        {
            Email = request.Email,
            Password = request.Password,
            Token = request.Token
        };
        var response = await _userService.ResetPasswordAsync(resetPasswordRequest);

        return Results.Ok(Result.Success(response));
    }
}
