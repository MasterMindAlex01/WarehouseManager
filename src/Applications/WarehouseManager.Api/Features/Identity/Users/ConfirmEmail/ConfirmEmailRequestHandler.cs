using MediatR;
using WarehouseManager.Application.Identity.Users;
using WarehouseManager.Shared.Wrapper;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmEmailRequestHandler : IRequestHandler<ConfirmEmailRequest, IResult>
{
    private readonly IUserService _userService;

    public ConfirmEmailRequestHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IResult> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService
            .ConfirmEmailAsync(request.UserId, request.Code, request.Tenant, cancellationToken);
        
        return Results.Ok(Result.Success(response));
    }
}
