using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException() : base("Product not found")
    {
        
    }
    public ProductNotFoundException(Guid key)  : base("Product", key)
    {
        
    }
}