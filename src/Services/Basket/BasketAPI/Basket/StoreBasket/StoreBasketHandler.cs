using Discount.Grpc;

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

public class StoreBasketCommandHandler(IBasketRepository repository, 
    DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient) 
    : ICommandHandler<StoreBasketCommand, StoreBasketCommandResult>
{
    public async Task<StoreBasketCommandResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // Add communication with Discount Microservice Using GRPC protocol
        await DeductDiscount(command, cancellationToken);
        // Store the basket the database (use marten upsert - update if exists or else create)
        var basket = await repository.StoreBasket(command.Cart, cancellationToken);
        // update the distributed cache
        return new StoreBasketCommandResult(basket.Username);
    }

    private async Task DeductDiscount(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        foreach (var item in command.Cart.Items)
        {
            var discount = await discountProtoServiceClient.GetDiscountAsync(new GetDiscountRequest()
            {
                ProductName = item.ProductName,
            }, cancellationToken: cancellationToken);
            item.Price -= discount.Amount;
        }
    }
}