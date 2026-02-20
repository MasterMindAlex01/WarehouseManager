using MediatR;

namespace WarehouseManager.Api.Features.Catalog.Brands;

public class GenerateRandomBrandRequest : IRequest<string>
{
    public int NSeed { get; set; }
}
