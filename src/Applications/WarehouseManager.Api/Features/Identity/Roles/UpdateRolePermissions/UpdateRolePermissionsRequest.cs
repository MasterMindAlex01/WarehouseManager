using MediatR;

namespace WarehouseManager.Api.Features.Identity.Roles;

public class UpdateRolePermissionsRequest : IRequest<IResult>
{
    public string RoleId { get; set; } = default!;
    public List<string> Permissions { get; set; } = default!;
}
