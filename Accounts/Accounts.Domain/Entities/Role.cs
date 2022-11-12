namespace Accounts.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }
    public IList<Account> Accounts { get; private set; } = new List<Account>();
}
