using Accounts.Api.ViewModels;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountsRepository _repository;

    public AccountController(IAccountsRepository repository)
        => _repository = repository;

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] SignUpViewModel viewModel)
    {
        if (viewModel.Password != viewModel.ConfirmPassword)
            return StatusCode(400, "The passwords don't match.");

        Account account = new(viewModel.Username, viewModel.Email, viewModel.Password);

        try
        {
            _repository.CreateAccount(account);

            return StatusCode(201, account);
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, "Username or email already registered.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult SignIn([FromBody] SignInViewModel viewModel)
    {
        try
        {
            Account? account = _repository.GetAccountByEmail(viewModel.Email);

            if (account == null || account.Password != viewModel.Password)
                return StatusCode(401, "The username or password is incorrect.");

            return StatusCode(200, account);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}