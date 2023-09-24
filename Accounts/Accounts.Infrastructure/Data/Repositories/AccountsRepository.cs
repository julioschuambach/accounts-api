using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AccountsDbContext _context;

    public AccountsRepository(AccountsDbContext context)
        => _context = context;

    public void CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public Account? GetAccountByEmail(string email)
    {
        var account = _context.Accounts
                              .AsNoTracking()
                              .Include(x => x.Roles)
                              .FirstOrDefault(x => x.Email == email);

        return account;
    }
}
