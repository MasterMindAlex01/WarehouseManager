using WarehouseManager.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace WarehouseManager.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = WHMPermission.NameFor(action, resource);
}