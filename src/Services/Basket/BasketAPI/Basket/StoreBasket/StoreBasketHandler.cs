namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketCommandResult(string Username);

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketCommandResult>;

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("{PropertyName} is required.");
        RuleFor(x => x.Cart.Username).NotNull().WithMessage("{PropertyName} is required.");
    }   
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public Task<StoreBasketCommandResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        // Store the basket the database (use marten upsert - update if exists or else create)
        // update the distributed cache
        return Task.FromResult(new StoreBasketCommandResult("Uzor"));
    }
}