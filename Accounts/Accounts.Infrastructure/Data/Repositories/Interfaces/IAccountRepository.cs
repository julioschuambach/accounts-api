using Accounts.Domain.Entities;

namespace Accounts.Infrastructure.Data.Repositories.Interfaces;

public interface IAccountRepository
{
    Account CreateAccount(Account account);
    Account GetAccountByEmail(string email);
    IEnumerable<Account> GetAccounts();
}
