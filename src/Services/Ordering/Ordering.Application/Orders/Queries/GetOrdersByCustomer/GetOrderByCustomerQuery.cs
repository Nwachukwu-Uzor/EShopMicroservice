namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrderByCustomerQueryResult(IEnumerable<OrderDto> Orders);

public record GetOrderByCustomerQuery(Guid CustomerId) : IQuery<GetOrderByCustomerQueryResult>;