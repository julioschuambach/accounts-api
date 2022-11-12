using System.ComponentModel.DataAnnotations;

namespace Accounts.Api.ViewModels;

public class SignUpViewModel
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}
