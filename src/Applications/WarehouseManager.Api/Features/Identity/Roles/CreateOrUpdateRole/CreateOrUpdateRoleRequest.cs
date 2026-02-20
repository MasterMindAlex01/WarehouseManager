using MediatR;

namespace WarehouseManager.Application.Identity.Roles;

public class CreateOrUpdateRoleRequest : IRequest<IResult>
{
    public string? Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}