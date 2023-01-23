namespace Omoqo.Challenge.Api.Core.Models;

public class UserAuthenticateModel
{
    public int Id { get; set; }
    public string ApiKey { get; set; } = string.Empty;
}