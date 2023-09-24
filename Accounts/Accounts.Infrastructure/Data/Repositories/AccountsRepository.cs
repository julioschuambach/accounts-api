using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly AccountsDbContext _context;

    public AccountsRepository(AccountsDbContext context)
        => _context = context;

    public async Task CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account?> GetAccountByEmail(string email)
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
