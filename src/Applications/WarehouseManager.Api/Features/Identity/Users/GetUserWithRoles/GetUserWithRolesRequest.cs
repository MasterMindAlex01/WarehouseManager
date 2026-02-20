using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserWithRolesRequest : IRequest<IResult>
{
    public string Id { get; }

    public GetUserWithRolesRequest(string id)
    {
        Id = id;
    }
}
