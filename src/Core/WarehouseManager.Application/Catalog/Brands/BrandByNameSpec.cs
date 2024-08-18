namespace WarehouseManager.Application.Catalog.Brands;

public class BrandByNameSpec : Specification<Brand>
{
    public BrandByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}