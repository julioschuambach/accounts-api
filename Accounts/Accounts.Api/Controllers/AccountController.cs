using Accounts.Api.Services;
using Accounts.Api.ViewModels;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _repository;
    private readonly TokenService _tokenService;

    public AccountController(IAccountRepository repository, TokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    [HttpPost]
    [Route("signup")]
    public IActionResult SignUp([FromBody] SignUpViewModel viewModel)
    {
        if (viewModel.Password != viewModel.ConfirmPassword)
            return StatusCode(400, new ResultViewModel<string>("As senhas não coincidem!"));

        Account newAccount = new(viewModel.Username, viewModel.Email, viewModel.Password);

        try
        {
            var account = _repository.CreateAccount(newAccount);

            return StatusCode(201, new ResultViewModel<Account>(account));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Nome de usuário e/ou e-mail já cadastrados!"));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
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
                return StatusCode(401, new ResultViewModel<string>("E-mail e/ou senha incorretos!"));

            var token = _tokenService.GenerateToken(account);

            return StatusCode(200, new ResultViewModel<string>(token, null));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
        }
    }

    [HttpGet]
    [Authorize(Roles = "Administrador")]
    public IActionResult GetAccounts()
    {
        try
        {
            var accounts = _repository.GetAccounts();

            return StatusCode(200, new ResultViewModel<IEnumerable<Account>>(accounts));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<string>("Falha interna no servidor!"));
        }
    }
}
