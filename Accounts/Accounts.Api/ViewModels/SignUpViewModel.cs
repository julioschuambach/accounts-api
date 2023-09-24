using System.ComponentModel.DataAnnotations;

namespace Accounts.Api.ViewModels;

public class SignUpViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Username { get; private set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; private set; }

    [Required]
    [StringLength(255, MinimumLength = 5)]
    public string Password { get; private set; }

    [Required]
    [StringLength(255, MinimumLength = 5)]
    public string ConfirmPassword { get; private set; }

    public SignUpViewModel(string username, string email, string password, string confirmPassword)
    {
        Username = username;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}