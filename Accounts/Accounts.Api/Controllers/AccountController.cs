using Accounts.Api.ViewModels;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountsDbContext _context;

    public AccountController(AccountsDbContext context)
        => _context = context;

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] SignUpViewModel viewModel)
    {
        if (viewModel.Password != viewModel.ConfirmPassword)
            return StatusCode(400, "The passwords don't match!");

        Account account = new(viewModel.Username, viewModel.Email, viewModel.Password);

        try
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return StatusCode(201, account);
        }
        catch
        {
            return StatusCode(500, "Internal server error!");
        }
    }
}