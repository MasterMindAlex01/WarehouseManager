namespace WarehouseManager.Application.Catalog.Products;

public class ProductByNameSpec : Specification<Product>
{
    public ProductByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}