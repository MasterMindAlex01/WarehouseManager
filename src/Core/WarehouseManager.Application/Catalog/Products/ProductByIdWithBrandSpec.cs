namespace WarehouseManager.Application.Catalog.Products;

public class ProductByIdWithBrandSpec : Specification<Product, ProductDetailsDto>
{
    public ProductByIdWithBrandSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Brand);
}