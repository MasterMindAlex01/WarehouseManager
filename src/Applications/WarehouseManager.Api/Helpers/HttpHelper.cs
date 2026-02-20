namespace WarehouseManager.Api.Helpers;

public class HttpExtensionHelper : IHttpExtensionHelper
{
    private readonly HttpContext? _httpContext;

    public HttpExtensionHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext;
    }

    public string GetOriginFromRequest()
        => $"{_httpContext?.Request.Scheme}://{_httpContext?.Request.Host.Value}{_httpContext?.Request.PathBase.Value}";

    public string GetIpAddress() =>
        _httpContext?.Request.Headers.ContainsKey("X-Forwarded-For") == true
            ? _httpContext.Request.Headers["X-Forwarded-For"]!
            : _httpContext?.Connection.RemoteIpAddress?.MapToIPv4().ToString() ?? "N/A";
}
