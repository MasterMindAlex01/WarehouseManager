using MediatR;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdRequest : IRequest<IResult>
{
    public string Id { get; }
    
    public GetRoleByIdRequest(string id)
    {
        Id = id;
    }

}
