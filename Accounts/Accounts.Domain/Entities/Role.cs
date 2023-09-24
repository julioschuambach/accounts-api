using Highscores.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Accounts.Domain.Entities;

public class Role : EntityBase
{
    public string Name { get; private set; }

    [JsonIgnore]
    public IList<Account> Accounts { get; private set; }

    public Role(string name)
    {
        Name = name;
        Accounts = new List<Account>();
    }
}