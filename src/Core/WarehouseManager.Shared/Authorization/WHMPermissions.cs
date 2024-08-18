using System.Collections.ObjectModel;

namespace WarehouseManager.Shared.Authorization;

public static class WHMAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class WHMResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class WHMPermissions
{
    private static readonly WHMPermission[] _all =
    [
        new("View Dashboard", WHMAction.View, WHMResource.Dashboard),
        new("View Hangfire", WHMAction.View, WHMResource.Hangfire),
        new("View Users", WHMAction.View, WHMResource.Users),
        new("Search Users", WHMAction.Search, WHMResource.Users),
        new("Create Users", WHMAction.Create, WHMResource.Users),
        new("Update Users", WHMAction.Update, WHMResource.Users),
        new("Delete Users", WHMAction.Delete, WHMResource.Users),
        new("Export Users", WHMAction.Export, WHMResource.Users),
        new("View UserRoles", WHMAction.View, WHMResource.UserRoles),
        new("Update UserRoles", WHMAction.Update, WHMResource.UserRoles),
        new("View Roles", WHMAction.View, WHMResource.Roles),
        new("Create Roles", WHMAction.Create, WHMResource.Roles),
        new("Update Roles", WHMAction.Update, WHMResource.Roles),
        new("Delete Roles", WHMAction.Delete, WHMResource.Roles),
        new("View RoleClaims", WHMAction.View, WHMResource.RoleClaims),
        new("Update RoleClaims", WHMAction.Update, WHMResource.RoleClaims),
        new("View Products", WHMAction.View, WHMResource.Products, IsBasic: true),
        new("Search Products", WHMAction.Search, WHMResource.Products, IsBasic: true),
        new("Create Products", WHMAction.Create, WHMResource.Products),
        new("Update Products", WHMAction.Update, WHMResource.Products),
        new("Delete Products", WHMAction.Delete, WHMResource.Products),
        new("Export Products", WHMAction.Export, WHMResource.Products),
        new("View Brands", WHMAction.View, WHMResource.Brands, IsBasic: true),
        new("Search Brands", WHMAction.Search, WHMResource.Brands, IsBasic: true),
        new("Create Brands", WHMAction.Create, WHMResource.Brands),
        new("Update Brands", WHMAction.Update, WHMResource.Brands),
        new("Delete Brands", WHMAction.Delete, WHMResource.Brands),
        new("Generate Brands", WHMAction.Generate, WHMResource.Brands),
        new("Clean Brands", WHMAction.Clean, WHMResource.Brands),
        new("View Tenants", WHMAction.View, WHMResource.Tenants, IsRoot: true),
        new("Create Tenants", WHMAction.Create, WHMResource.Tenants, IsRoot: true),
        new("Update Tenants", WHMAction.Update, WHMResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", WHMAction.UpgradeSubscription, WHMResource.Tenants, IsRoot: true)
    ];

    public static IReadOnlyList<WHMPermission> All { get; } = new ReadOnlyCollection<WHMPermission>(_all);
    public static IReadOnlyList<WHMPermission> Root { get; } = new ReadOnlyCollection<WHMPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<WHMPermission> Admin { get; } = new ReadOnlyCollection<WHMPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<WHMPermission> Basic { get; } = new ReadOnlyCollection<WHMPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record WHMPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
