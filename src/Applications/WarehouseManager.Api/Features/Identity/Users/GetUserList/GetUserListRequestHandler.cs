using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserListRequestHandler : IRequestHandler<GetUserListRequest, IResult>
{
    private readonly IUserService _userService;

    public GetUserListRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(GetUserListRequest request, CancellationToken cancellationToken)
    {
        List<UserDetailsDto> users = await _userService.GetListAsync(cancellationToken);
        return Results.Ok(Result<List<UserDetailsDto>>.Success(users, "Ok"));
    }
}
