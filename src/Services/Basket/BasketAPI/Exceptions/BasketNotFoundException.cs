namespace BasketAPI.Exceptions;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string username) : base("Basket", username)
    {
        
    }

    public BasketNotFoundException() : base("Basket not found")
    {
        
    }
}