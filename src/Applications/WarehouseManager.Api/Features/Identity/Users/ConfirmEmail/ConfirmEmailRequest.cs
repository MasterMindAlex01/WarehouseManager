using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class ConfirmEmailRequest : IRequest<IResult>
{
    public ConfirmEmailRequest(string tenant, string userId, string code)
    {
        Tenant = tenant;
        UserId = userId;
        Code = code;
    }

    public string Tenant { get; }
    public string UserId { get; }
    public string Code { get; }
}
