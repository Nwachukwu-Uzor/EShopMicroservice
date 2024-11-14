namespace BasketAPI.Basket.DeleteBasket;

public record DeleteBasketCommandResult(bool IsSuccess);
public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketCommandResult>;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
    }
}

public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new DeleteBasketCommandResult(true));
    }
}