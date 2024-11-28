namespace Ordering.Domain.ValueObjects;

public record CustomerId
{
    public Guid Value { get; }
    private CustomerId(Guid value) => Value = value;

    public static CustomerId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);
        if (id == Guid.Empty)
        {
            throw new DomainException("Invalid customer id");
        }

        return new CustomerId(id);
    }
};