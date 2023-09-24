using Accounts.Domain.Entities;
using Accounts.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Accounts.Infrastructure.Data;

public class AccountsDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AccountsDbContext(IConfiguration configuration)
        => _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Default"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountMapping());
        modelBuilder.ApplyConfiguration(new RoleMapping());
    }
}