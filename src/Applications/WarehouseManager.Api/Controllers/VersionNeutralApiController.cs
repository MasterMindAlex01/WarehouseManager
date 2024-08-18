using Asp.Versioning;

namespace WarehouseManager.Host.Controllers;

[Route("api/[controller]")]
[ApiVersionNeutral]
public class VersionNeutralApiController : BaseApiController
{
}
