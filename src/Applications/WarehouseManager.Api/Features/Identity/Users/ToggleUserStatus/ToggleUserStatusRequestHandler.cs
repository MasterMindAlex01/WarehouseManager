using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ToggleUserStatusRequestHandler : IRequestHandler<ToggleUserStatusRequest, IResult>
{
    private readonly IUserService _userService;

    public ToggleUserStatusRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var toggleUserStatusRequestDto = new ToggleUserStatusRequestDto
        {
            ActivateUser = request.ActivateUser ?? false,
            UserId = request.UserId
        };
        await _userService.ToggleStatusAsync(toggleUserStatusRequestDto, cancellationToken);

        return Results.Ok(Result.Success($"user {request.UserId} status toggled"));
    }
}
