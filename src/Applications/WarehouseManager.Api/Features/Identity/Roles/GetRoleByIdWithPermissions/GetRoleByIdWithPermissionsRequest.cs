using MediatR;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class GetRoleByIdWithPermissionsRequest : IRequest<IResult>
{
    public string Id { get; }

    public GetRoleByIdWithPermissionsRequest(string id)
    {
        Id = id;
    }
}
