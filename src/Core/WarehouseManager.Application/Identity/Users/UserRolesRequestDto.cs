namespace WarehouseManager.Application.Identity.Users;

public class UserRolesRequestDto
{
    public string UserId { get; set; } = string.Empty;
    public List<UserRoleDto> UserRoles { get; set; } = new();
}

