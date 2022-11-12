using Accounts.Api.ViewModels;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repository;

    public AccountController(IAccountRepository repository)
        => _repository = repository;

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] SignUpViewModel viewModel)
    {
        if (viewModel.Password != viewModel.ConfirmPassword)
            return StatusCode(400, "As senhas não coincidem!");

        Account newAccount = new(viewModel.Username, viewModel.Email, viewModel.Password);

        try
        {
            var account = _repository.CreateAccount(newAccount);

            return StatusCode(201, account);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor!");
        }
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult SignIn([FromBody] SignInViewModel viewModel)
    {
        try
        {
            var account = _repository.GetAccountByEmail(viewModel.Email);

            if (account == null || account.Password != viewModel.Password)
                return StatusCode(401, "E-mail e/ou senha incorretos!");

            return StatusCode(200, account);
        }
        catch
        {
            return StatusCode(500, "Falha interna no servidor!");
        }
    }
}
