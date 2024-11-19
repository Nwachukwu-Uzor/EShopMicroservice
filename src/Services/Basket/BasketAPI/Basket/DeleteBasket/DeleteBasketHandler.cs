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

public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public async  Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteBasket(command.Username, cancellationToken);
        return new DeleteBasketCommandResult(true);
    }
}