namespace Accounts.Domain.Entities;

public class Account : Entity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public DateTime Created { get; private set; } = DateTime.Now;
    public DateTime LastUpdated { get; private set; } = DateTime.Now;
    public IList<Role> Roles { get; private set; } = new List<Role>();
}
