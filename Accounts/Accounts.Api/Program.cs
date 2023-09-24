using Accounts.Infrastructure.Data;

namespace Accounts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();

            builder.Services.AddDbContext<AccountsDbContext>();

            var app = builder.Build();
            app.MapControllers();

            app.Run();
        }
    }
}