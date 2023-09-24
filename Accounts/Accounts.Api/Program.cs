using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Data.Interfaces.Repositories;
using Accounts.Infrastructure.Data.Repositories;

namespace Accounts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AccountsDbContext>();
            builder.Services.AddTransient<IAccountsRepository, AccountsRepository>();

            var app = builder.Build();
            app.MapControllers();

            app.Run();
        }
    }
}