namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public CustomerId CustomerId { get; set; } = default!;
    public OrderName OrderName { get; private set; } = default!;
    public Address ShippingAddress { get; set; }
    public Address BillingAddress { get; set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public Payment Payment { get; set; }

    public decimal TotalPrice
    {
        get => OrderItems.Sum(o => o.Price * o.Quantity);
        private set { }
    }

    public static Order Create(
        OrderId id, CustomerId customerId, OrderName orderName, Address shippingAddress, 
        Address billingAddress, Payment payment)
    {
        var order = new Order()
        {
            Id = id,
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Status = OrderStatus.Pending,
            Payment = payment,
        };
        order.AddDomainEvent(new OrderCreatedEvent(order));
        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress, Address billingAddress, 
        Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;
        AddDomainEvent(new OrderUpdatedEvent(this));
    }

    public void Add(ProductId productId, decimal price, int quantity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
        var orderItem = new OrderItem(Id, productId, price, quantity);
        _orderItems.Add(orderItem);
    }

    public void Remove(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is null)
        {
            throw new ArgumentException("Product not found", nameof(productId));
        }
        _orderItems.Remove(orderItem);
    }
}