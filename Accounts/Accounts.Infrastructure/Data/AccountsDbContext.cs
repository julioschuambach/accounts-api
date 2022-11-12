using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Data;

public class AccountsDbContext : DbContext
{
    private readonly string _connectionString = "Server = localhost, 1433; Database = AccountsDb; User ID = sa; Password = 1q2w3e4r@#$;";
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_connectionString);
}
