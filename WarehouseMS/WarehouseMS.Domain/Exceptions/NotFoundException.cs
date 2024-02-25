namespace WarehouseMS.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("User is not authenticated.")
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }
}