using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, IResult>
{
    private readonly IUserService _userService;

    public GetUserByIdRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        UserDetailsDto user = await _userService.GetAsync(request.Id, cancellationToken);

        return Results.Ok(Result<UserDetailsDto>.Success(user, "Ok"));
    }
}
