namespace WarehouseManager.Api.Filters;

public static class PermissionExtensions
{
    public static RouteHandlerBuilder RequirePermission(this RouteHandlerBuilder endpoint,
        string action, string resource)
        => endpoint.RequireAuthorization(WHMPermission.NameFor(action, resource));
}
