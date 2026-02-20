namespace WarehouseManager.Api.Features.Catalog.Products;

public class SearchProductsRequestDto : PaginationFilter
{
    public Guid? BrandId { get; set; }
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}