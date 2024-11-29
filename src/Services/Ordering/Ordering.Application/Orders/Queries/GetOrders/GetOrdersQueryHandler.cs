using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersQueryHandler(IApplicationDbContext context) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
{
    public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        int page = query.PaginationRequest.Page;
        int pageSize = query.PaginationRequest.PageSize;
        var count = await context.Orders.LongCountAsync(cancellationToken);
        var orders = await context.Orders.Include(o => o.OrderItems)
            .AsNoTracking()
            .OrderBy(o => o.CreatedAt)
            .Skip(pageSize * page)
            .Take(pageSize).ToListAsync(cancellationToken);
        var orderDtos = orders.ToOrderDtoList();
        return new GetOrdersQueryResult(new PaginationResult<OrderDto>(page, pageSize, count, orderDtos));
    }
}