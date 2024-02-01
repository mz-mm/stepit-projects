namespace WarehouseMS.Domain.Exceptions;

public class NotAuthenticatedException : Exception
{
    public NotAuthenticatedException() : base("User is not authenticated.")
    {
    }

    public NotAuthenticatedException(string message) : base(message)
    {
    }
}