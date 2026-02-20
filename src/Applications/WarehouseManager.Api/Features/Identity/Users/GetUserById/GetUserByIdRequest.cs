using MediatR;

namespace WarehouseManager.Api.Features.Identity.Users;

public class GetUserByIdRequest : IRequest<IResult>
{
    public string Id { get; }

    public GetUserByIdRequest(string id)
    {
        Id = id;
    }

}
