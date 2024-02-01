namespace WarehouseMS.Domain.Exceptions;

public class PasswordNotMatchException : Exception
{
    public PasswordNotMatchException() : base("Passwords not match.")
    {
    }

    public PasswordNotMatchException(string message) : base(message)
    {
    }
}