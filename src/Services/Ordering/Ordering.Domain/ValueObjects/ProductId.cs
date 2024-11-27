namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid productId)
    {
        ArgumentNullException.ThrowIfNull(productId);
        if (productId == Guid.Empty)
        {
            throw new DomainException("Product Id cannot be empty");
        }

        return new ProductId(productId);
    }
};