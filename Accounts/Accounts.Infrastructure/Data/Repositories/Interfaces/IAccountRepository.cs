using Accounts.Domain.Entities;

namespace Accounts.Infrastructure.Data.Repositories.Interfaces;

public interface IAccountRepository
{
    Task<Account> CreateAccount(Account account);
    Task<Account> GetAccountByEmail(string email);
    Task<IEnumerable<Account>> GetAccounts();
}
