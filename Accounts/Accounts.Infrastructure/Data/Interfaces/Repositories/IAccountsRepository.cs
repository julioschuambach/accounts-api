using Accounts.Domain.Entities;

namespace Accounts.Infrastructure.Data.Interfaces.Repositories;

public interface IAccountsRepository
{
    void CreateAccount(Account account);
    Account? GetAccountByEmail(string email);
    IEnumerable<Account> GetAccounts();
}
