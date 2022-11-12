using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AccountsDbContext _context;

    public AccountRepository(AccountsDbContext context)
        => _context = context;

    public async Task<Account> CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Account> GetAccountByEmail(string email)
    {
        var account = await _context.Accounts
                                        .AsNoTracking()
                                        .Include(x => x.Roles)
                                        .FirstOrDefaultAsync(x => x.Email == email);

        return account;
    }

    public async Task<IEnumerable<Account>> GetAccounts()
    {
        var accounts = await _context.Accounts
                                        .AsNoTracking()
                                        .Include(x => x.Roles)
                                        .ToListAsync();

        return accounts;
    }
}
