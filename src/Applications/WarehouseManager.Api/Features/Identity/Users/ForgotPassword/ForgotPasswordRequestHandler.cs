using MediatR;
using WarehouseManager.Api.Helpers;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ForgotPasswordRequestHandler : IRequestHandler<ForgotPasswordRequest, IResult>
{
    private readonly IUserService _userService;
    private readonly IHttpExtensionHelper _httpExtensionHelper;

    public ForgotPasswordRequestHandler(
        IUserService userService,
        IHttpExtensionHelper httpExtensionHelper)
    {
        _userService = userService;
        _httpExtensionHelper = httpExtensionHelper;
    }

    public async Task<IResult> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var forgotPasswordRequestDto = new ForgotPasswordRequestDto
        {
            Email = request.Email
        };
        var response = await _userService.ForgotPasswordAsync(forgotPasswordRequestDto, _httpExtensionHelper.GetOriginFromRequest());

        return Results.Ok(Result.Success(response));
    }
}
