using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmPhoneNumberRequestHandler : IRequestHandler<ConfirmPhoneNumberRequest, IResult>
{
    private readonly IUserService _userService;

    public ConfirmPhoneNumberRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(ConfirmPhoneNumberRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService
            .ConfirmPhoneNumberAsync(request.UserId, request.Code);
        
        return Results.Ok(Result.Success(response));
    }
}
