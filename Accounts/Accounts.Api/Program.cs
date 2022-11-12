using Accounts.Infrastructure.Data;
using Accounts.Infrastructure.Data.Repositories;
using Accounts.Infrastructure.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<AccountsDbContext>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

var app = builder.Build();
app.MapControllers();

app.Run();
