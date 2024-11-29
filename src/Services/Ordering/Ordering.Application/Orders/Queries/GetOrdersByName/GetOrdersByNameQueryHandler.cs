using Mapster;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameQueryResult>
{
    public async Task<GetOrdersByNameQueryResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await context
            .Orders.Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(order => order.OrderName.Value.Contains(query.OrderName))
            .OrderBy(order => order.OrderName.Value)
            .ToListAsync(cancellationToken);

        var orderDtos = orders.ToOrderDtoList();
        return new GetOrdersByNameQueryResult(orderDtos);
    }
}