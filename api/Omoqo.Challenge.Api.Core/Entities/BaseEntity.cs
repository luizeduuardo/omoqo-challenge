namespace Omoqo.Challenge.Api.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }

    [JsonIgnore]
    [NotMapped]
    public List<string> Errors { get; protected set; } = new List<string>();

    [JsonIgnore]
    [NotMapped]
    public bool IsValid => !Errors.Any();
}