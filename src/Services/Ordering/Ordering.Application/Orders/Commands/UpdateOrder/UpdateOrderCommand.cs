namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommandResult(bool isSuccess);

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderCommandResult>;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Order.OrderName).NotNull().WithMessage("{PropertyName} is required");
        RuleFor(c => c.Order.Id).NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(c => c.Order.CustomerId).NotNull().WithMessage("{PropertyName} is required");
    }
}