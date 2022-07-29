using Microsoft.AspNetCore.Identity;

namespace PropertySales.SecureAuth.Exceptions;

public class CreateUserException : Exception
{
    public IEnumerable<IdentityError>? Errors { get; private set; }

    public CreateUserException(string message) : base(message)
    {
        Errors = new List<IdentityError>();
    }

    public CreateUserException(IList<IdentityError> errors) 
        : base($"{MakeErrorMessage(errors)}")
    {
        Errors = errors;
    }

    public CreateUserException(string message, IList<IdentityError> errors)
        : base($"{message} {MakeErrorMessage(errors)}")
    {
        Errors = errors;
    }

    private static string MakeErrorMessage(IEnumerable<IdentityError> errors)
    {
        var arr = errors.Select(x => $"{Environment.NewLine} -- {x.Code}: {x.Description}");
        return "Validation failed: " + string.Join(string.Empty, arr);
    }
}
