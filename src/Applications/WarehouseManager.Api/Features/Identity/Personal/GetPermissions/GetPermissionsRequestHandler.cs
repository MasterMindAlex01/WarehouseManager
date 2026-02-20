using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Personal.GetPermissions;

public class GetPermissionsRequestHandler : IRequestHandler<GetPermissionsRequest, IResult>
{
    private readonly IUserService _userService;

    public GetPermissionsRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(GetPermissionsRequest request, CancellationToken cancellationToken)
    {
        var permissions = await _userService.GetPermissionsAsync(request.UserId, cancellationToken);
        return Results.Ok(Result<List<string>>.Success(permissions));
    }
}
