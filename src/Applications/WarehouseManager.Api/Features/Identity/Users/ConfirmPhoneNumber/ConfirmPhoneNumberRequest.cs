using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmPhoneNumberRequest : IRequest<IResult>
{
    public ConfirmPhoneNumberRequest(string userId, string code)
    {
        UserId = userId;
        Code = code;
    }

    public string UserId { get; }
    public string Code { get; }
}
