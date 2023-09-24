using Accounts.Api.Extensions;
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
        if (!ModelState.IsValid)
            return StatusCode(400, new ResultViewModel<string>(ModelState.GetErrors()));

        if (viewModel.Password != viewModel.ConfirmPassword)
            return StatusCode(400, new ResultViewModel<string>("The passwords don't match."));

        Account account = new(viewModel.Username, viewModel.Email, viewModel.Password);

        try
        {
            _repository.CreateAccount(account);

            return StatusCode(201, new ResultViewModel<Account>(account));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Username or email already registered."));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpPost]
    [Route("signin")]
    public IActionResult SignIn([FromBody] SignInViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return StatusCode(400, new ResultViewModel<string>(ModelState.GetErrors()));

        try
        {
            Account? account = _repository.GetAccountByEmail(viewModel.Email);

            if (account == null || account.Password != viewModel.Password)
                return StatusCode(401, new ResultViewModel<string>("The username or password is incorrect."));

            var token = _tokenService.GenerateToken(account);

            return StatusCode(200, new ResultViewModel<string>(token, new()));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }

    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult GetAccounts()
    {
        try
        {
            var accounts = _repository.GetAccounts();

            return StatusCode(200, new ResultViewModel<IEnumerable<Account>>(accounts));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>(ex.Message));
        }
    }
}