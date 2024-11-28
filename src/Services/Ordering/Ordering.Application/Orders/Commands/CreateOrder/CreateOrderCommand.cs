namespace Ordering.Application.Orders.Commands.CreateOrder;


public record CreateOrderCommandResult(Guid Id);

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderCommandResult>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(p => p.Order.OrderName).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(c => c.Order.OrderItems).NotEmpty().WithMessage("{PropertyName} is required");
    }
}