using Highscores.Domain.Entities.Base;

namespace Accounts.Domain.Entities;

public class Account : EntityBase
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime LastUpdatedDate { get; private set; }
    public IList<Role> Roles { get; private set; }

    public Account(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
        CreatedDate = DateTime.Now;
        LastUpdatedDate = DateTime.Now;
        Roles = new List<Role>();
    }
}