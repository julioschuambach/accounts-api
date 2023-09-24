using Accounts.Api.Services;
using Accounts.Api.ViewModels;
using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Api.Controllers;

[ApiController]
[Route("accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountsRepository _repository;
    private readonly TokenService _tokenService;

    public AccountController(IAccountsRepository repository, TokenService tokenService)
    { 
        _repository = repository;
        _tokenService = tokenService;
    }

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

            var token = _tokenService.GenerateToken(account);

            return StatusCode(200, token);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult GetAccounts()
    {
        try
        {
            var accounts = _repository.GetAccounts();

            return StatusCode(200, accounts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}