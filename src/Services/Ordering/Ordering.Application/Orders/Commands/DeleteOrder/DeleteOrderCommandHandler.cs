namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IApplicationDbContext context) : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
{
    public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        // Retrieve order from the database using it's id
        var existingOrder = await context.Orders.FindAsync([OrderId.Of(command.OrderId)], cancellationToken);
        if (existingOrder is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }
        // Remove the order from the database
        context.Orders.Remove(existingOrder);
        await context.SaveChangesAsync(cancellationToken);
        // Return result to the calling client
        return new DeleteOrderCommandResult(true);
    }
}