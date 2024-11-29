using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrderByCustomerQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrderByCustomerQuery, GetOrderByCustomerQueryResult>
{
    public async Task<GetOrderByCustomerQueryResult> Handle(GetOrderByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await context.Orders.Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
            .ToListAsync(cancellationToken);
        return new GetOrderByCustomerQueryResult(orders.ToOrderDtoList());
    }
}