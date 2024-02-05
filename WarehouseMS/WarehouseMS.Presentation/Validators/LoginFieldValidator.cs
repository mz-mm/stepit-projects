using System;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using PostSharp.Reflection.MethodBody;

namespace WarehouseMS.Presentation.Validators;

public class LoginFieldValidator
{
    public static bool IsEmailValid(string email)
    {
        var emailRegex = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

        if (!string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, emailRegex))
            return true;

        throw new InvalidCredentialException("Incorrect email or password");

    }

    public static bool IsPasswordValid(string password)
    {
        if (!string.IsNullOrWhiteSpace(password) && password.Length >= 8)
            return true;

        throw new InvalidCredentialException("Incorrect email or password");
    }
}