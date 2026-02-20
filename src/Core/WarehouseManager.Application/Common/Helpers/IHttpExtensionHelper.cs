namespace WarehouseManager.Api.Helpers;

public interface IHttpExtensionHelper: ITransientService
{
    string GetIpAddress();
    string GetOriginFromRequest();
}