using System.ComponentModel.DataAnnotations;

namespace Accounts.Api.ViewModels;

public class SignInViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; private set; }

    [Required]
    public string Password { get; private set; }

    public SignInViewModel(string email, string password)
    {
        Email = email;
        Password = password;
    }
}