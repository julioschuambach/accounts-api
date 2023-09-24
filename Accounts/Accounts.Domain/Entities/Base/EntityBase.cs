using System.Text.Json.Serialization;

namespace Highscores.Domain.Entities.Base;

public abstract class EntityBase
{
    [JsonIgnore]
    public Guid Id { get; protected set; }

    public EntityBase()
    {
        Id = Guid.NewGuid();
    }
}