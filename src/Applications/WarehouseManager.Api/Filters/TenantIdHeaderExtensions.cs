using WarehouseManager.Shared.Multitenancy;

namespace WarehouseManager.Api.Filters;

public static class TenantIdHeaderExtensions
{
    private const string HeaderName = MultitenancyConstants.TenantIdName;

    public static RouteHandlerBuilder RequireTenantIdHeader(this RouteHandlerBuilder builder)
    {
        builder.Add(endpointBuilder =>
        {
            endpointBuilder.FilterFactories.Add((context, next) =>
                async invocationContext =>
                {
                    var http = invocationContext.HttpContext;

                    if (!http.Request.Headers.TryGetValue(HeaderName, out var _))
                    {
                        http.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await http.Response.WriteAsync("Missing Tenant Id header");
                        return null;
                    }

                    return await next(invocationContext);
                });
        });

        return builder;
    }
}
