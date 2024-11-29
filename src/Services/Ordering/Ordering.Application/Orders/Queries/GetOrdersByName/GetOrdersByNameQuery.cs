namespace Ordering.Application.Orders.Queries.GetOrderByName;

public record GetOrdersByNameQueryResult(IEnumerable<OrderDto> Orders);

public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameQueryResult>;

public class GetOrdersByNameQueryValidator : AbstractValidator<GetOrdersByNameQuery>
{
    public GetOrdersByNameQueryValidator()
    {
        RuleFor(v => v.OrderName).NotEmpty().MaximumLength(50);
    }
}