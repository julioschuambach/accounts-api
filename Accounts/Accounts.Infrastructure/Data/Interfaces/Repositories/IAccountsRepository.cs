using Accounts.Domain.Entities;

namespace Accounts.Infrastructure.Data.Interfaces.Repositories;

public interface IAccountsRepository
{
    Task CreateAccount(Account account);
    Task<Account?> GetAccountByEmail(string email);
    Task<IEnumerable<Account>> GetAccounts();
}
