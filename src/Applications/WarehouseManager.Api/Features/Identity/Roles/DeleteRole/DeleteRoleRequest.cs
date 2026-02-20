using MediatR;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class DeleteRoleRequest : IRequest<IResult>
{
    public string Id { get; }

    public DeleteRoleRequest(string id)
    {
        Id = id;
    }
}
