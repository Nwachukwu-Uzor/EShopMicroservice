using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace BasketAPI.Basket.CheckoutBasket;

public record CheckoutBasketCommandResult(bool IsSuccess);

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketCommandResult>;

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(c => c.BasketCheckoutDto).NotNull().WithMessage("{PropertyName} is required.}");
        RuleFor(c => c.BasketCheckoutDto.Username).NotEmpty().WithMessage("{PropertyName} is required.");
    }
}

public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketCommandResult>
{
    public async Task<CheckoutBasketCommandResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        var existingBasket = await basketRepository.GetBasket(command.BasketCheckoutDto.Username, cancellationToken);
        if (existingBasket is null)
        {
            return new CheckoutBasketCommandResult(false);
        }

        var eventMessage = command.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        // set total price on basket checkout event message
        eventMessage.TotalPrice = existingBasket.TotalPrice;
        // send basket checkout event to RabbitMq using mass transit
        await publishEndpoint.Publish(eventMessage, cancellationToken);
        // delete the basket
        await basketRepository.DeleteBasket(command.BasketCheckoutDto.Username, cancellationToken);
        return new CheckoutBasketCommandResult(true);
    }
}