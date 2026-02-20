using MediatR;

namespace WarehouseManager.Api.Features.Identity.Personal;

public class GetPermissionsRequest : IRequest<IResult>
{
    public GetPermissionsRequest(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}
