using System.Text.Json.Serialization;

namespace Accounts.Domain.Entities;

public abstract class Entity
{
    [JsonIgnore]
    public Guid Id { get; private set; } = Guid.NewGuid();
}
