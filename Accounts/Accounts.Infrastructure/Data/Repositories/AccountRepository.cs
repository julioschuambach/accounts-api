using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountsDbContext _context;

    public AccountRepository(AccountsDbContext context)
        => _context = context;

    public Account CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();

        return account;
    }

    public Account GetAccountByEmail(string email)
    {
        var account = _context.Accounts
                                .AsNoTracking()
                                .Include(x => x.Roles)
                                .FirstOrDefault(x => x.Email == email);

        return account;
    }
}
