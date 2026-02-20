using MediatR;
using WarehouseManager.Application.Identity.Users;

namespace WarehouseManager.Api.Features.Identity.Users;

public class UserRolesRequest : IRequest<IResult>
{
    public string UserId { get; set; } = string.Empty;
    public List<UserRoleDto> UserRoles { get; set; } = new();
}

