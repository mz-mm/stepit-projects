using System.Security.Authentication;
using System.Text.RegularExpressions;

namespace WarehouseMS.Presentation.Validators;

public class FieldValidator
{
    private static readonly Regex EmailRegex = new(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
    private static readonly Regex NameRegex = new(@"^[a-zA-Z]+$");

    public static bool ValidateEmail(string email, string exMessage = "Incorrect email or password")
    {
        if (!string.IsNullOrWhiteSpace(email) && EmailRegex.IsMatch(email))
            return true;

        throw new InvalidCredentialException(exMessage);

    }

    public static bool ValidatePassword(string password, string exMessage = "Incorrect email or password")
    {
        if (!string.IsNullOrWhiteSpace(password) && password.Length >= 8)
            return true;

        throw new InvalidCredentialException(exMessage);
    }

    public static bool ValidateName(string name, string exMessage = "Invalid Name")
    {
        if (!string.IsNullOrWhiteSpace(name) && NameRegex.IsMatch(name) && name.Length <= 50 )
            return true;

        throw new InvalidCredentialException(exMessage);
    }
}