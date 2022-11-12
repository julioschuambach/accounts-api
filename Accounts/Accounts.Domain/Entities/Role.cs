using System.Text.Json.Serialization;

namespace Accounts.Domain.Entities;

public class Role : Entity
{
    public string Name { get; private set; }

    [JsonIgnore]
    public IList<Account> Accounts { get; private set; } = new List<Account>();
}
