using Accounts.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<AccountsDbContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
